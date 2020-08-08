using MachineLearning_SimpleCloud_Broker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MachineLearning_SimpleCloud_Broker.Infrastructure.Persistence.Configurations
{
    public class ServiceDetailsConfiguration : IEntityTypeConfiguration<ServiceDetails>
    {
        public void Configure(EntityTypeBuilder<ServiceDetails> builder)
        {
            //builder
            //    .HasOne(mlService => mlService)
            //    .WithMany(serviceDetails => serviceDetails.ServiceTasks);
                // .HasForeignKey<ServiceTask>(serviceTask => serviceTask.);
        }
    }
}
