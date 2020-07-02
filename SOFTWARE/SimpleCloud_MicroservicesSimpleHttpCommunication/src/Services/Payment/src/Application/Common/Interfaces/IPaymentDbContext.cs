using Payment_SimpleCloud_MicroservicesHttp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Payment_SimpleCloud_MicroservicesHttp.Application.Common.Interfaces
{
    public interface IPaymentDbContext
    {
        //DbSet<TodoList> TodoLists { get; set; }

        //DbSet<TodoItem> TodoItems { get; set; }

        DbSet<Client> Clients { get; set; }
        DbSet<ClientTask> ServiceTasks { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        void Upsert(object entity);
        // Task SaveChangesAsync();
    }
}
