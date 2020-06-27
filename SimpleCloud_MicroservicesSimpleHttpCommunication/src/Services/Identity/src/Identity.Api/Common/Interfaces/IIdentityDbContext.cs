using Microsoft.EntityFrameworkCore;
using Identity_SimpleCloud_MicroservicesHttp.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleCloudMonolithic.Application.Common.Interfaces
{
    public interface IIdentityDbContext
    {
        //DbSet<TodoList> TodoLists { get; set; }

        //DbSet<TodoItem> TodoItems { get; set; }

        DbSet<Client> Clients { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        void Upsert(object entity);
        // Task SaveChangesAsync();
    }
}
