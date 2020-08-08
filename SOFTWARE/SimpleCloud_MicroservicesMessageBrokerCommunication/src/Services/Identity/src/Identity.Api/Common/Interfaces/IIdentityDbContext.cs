using Microsoft.EntityFrameworkCore;
using Identity_SimpleCloud_MicroservicesMessageBroker.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Identity_SimpleCloud_MicroservicesMessageBroker.Application.Common.Interfaces
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
