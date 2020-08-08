using Identity_SimpleCloud_MicroservicesHttp.Application.Common.Interfaces;
using Identity_SimpleCloud_MicroservicesHttp.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Identity_SimpleCloud_MicroservicesHttp.Domain.Entities;
using System.Diagnostics;

namespace Identity_SimpleCloud_MicroservicesHttp.Infrastructure.Persistence
{
    public class IdentityDbContext : DbContext, IIdentityDbContext
    {
        private readonly IDateTime _dateTime;
        private IDbContextTransaction _currentTransaction;

        public DbSet<Client> Clients { get; set; }

        public IdentityDbContext() { }

        public IdentityDbContext(
            DbContextOptions<IdentityDbContext> options)
            :base(options)
        {
            // _dateTime = dateTime;
        }
        
       
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        //entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.CreatedBy = "testUserId";
                        entry.Entity.Created = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        //entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModifiedBy = "testUserId";
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                return;
            }

            _currentTransaction = await base.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted).ConfigureAwait(false);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync().ConfigureAwait(false);

                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void Upsert(object entity)
        {
            ChangeTracker.TrackGraph(entity, e =>
            {
                if (e.Entry.IsKeySet)
                {
                    e.Entry.State = EntityState.Modified;
                }
                else
                {
                    e.Entry.State = EntityState.Added;
                }
            });

#if DEBUG
            foreach (var entry in ChangeTracker.Entries())
            {
                Debug.WriteLine($"Entity: {entry.Entity.GetType().Name} State: {entry.State.ToString()}");
            }
#endif
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
