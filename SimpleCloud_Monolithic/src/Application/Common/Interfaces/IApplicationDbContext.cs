using SimpleCloudMonolithic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using SimpleCloud_Monolithic.Domain.Entities;

namespace SimpleCloudMonolithic.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        //DbSet<TodoList> TodoLists { get; set; }

        //DbSet<TodoItem> TodoItems { get; set; }

        DbSet<Client> Clients { get; set; }
        DbSet<OrderedMLService> OrderedServices { get; set; }
        DbSet<ServiceDetails> ServiceDetails { get; set; }
        DbSet<ServiceTask> ServiceTasks { get; set; }
        DbSet<File> Files { get; set; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
