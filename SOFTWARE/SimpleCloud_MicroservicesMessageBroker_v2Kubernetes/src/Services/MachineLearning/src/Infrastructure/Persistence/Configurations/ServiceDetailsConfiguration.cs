using MachineLearning_SimpleCloud_MicroservicesHttp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MachineLearning_SimpleCloud_MicroservicesHttp.Infrastructure.Persistence.Configurations
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
