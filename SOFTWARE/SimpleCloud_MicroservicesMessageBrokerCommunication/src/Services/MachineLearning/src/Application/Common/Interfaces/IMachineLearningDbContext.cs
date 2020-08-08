using MachineLearning_SimpleCloud_Broker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using MachineLearning_SimpleCloud_Broker.Domain.Entities;

namespace MachineLearning_SimpleCloud_Broker.Application.Common.Interfaces
{
    public interface IMachineLearningDbContext
    {
        //DbSet<TodoList> TodoLists { get; set; }

        //DbSet<TodoItem> TodoItems { get; set; }

        DbSet<Client> Clients { get; set; }
        DbSet<MLService> MLServices { get; set; }
        DbSet<ServiceDetails> ServiceDetails { get; set; }
        DbSet<ServiceTask> ServiceTasks { get; set; }
        DbSet<File> Files { get; set; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        void Upsert(object entity);
        // Task SaveChangesAsync();
    }
}
