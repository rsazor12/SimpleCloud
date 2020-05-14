using SimpleCloudMonolithic.Application.Common.Interfaces;
using SimpleCloudMonolithic.Domain.Common;
using SimpleCloudMonolithic.Domain.Entities;
using SimpleCloudMonolithic.Infrastructure.Identity;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using SimpleCloud_Monolithic.Domain.Entities;
using System.Diagnostics;
using Tensorflow.Eager;

namespace SimpleCloudMonolithic.Infrastructure.Persistence
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>,  IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private IDbContextTransaction _currentTransaction;

        public DbSet<Client> Clients { get; set; }
        public DbSet<MLService> MLServices { get; set; }
        public DbSet<ServiceDetails> ServiceDetails { get; set; }
        public DbSet<ServiceTask> ServiceTasks { get; set; }
        public DbSet<File> Files { get; set; }

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions,
            ICurrentUserService currentUserService,
            IDateTime dateTime) : base(options, operationalStoreOptions)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;

            // Context.
            // ChangeTracker.LazyLoadingEnabled = false;
        }

        //public DbSet<TodoList> TodoLists { get; set; }

        //public DbSet<TodoItem> TodoItems { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
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

            //context.Entry(employee.Address).State = EntityState.Detached;
            //employee.SetAddress(newAddress);
            //context.Entry(employee.Address).State = EntityState.Modified;

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
