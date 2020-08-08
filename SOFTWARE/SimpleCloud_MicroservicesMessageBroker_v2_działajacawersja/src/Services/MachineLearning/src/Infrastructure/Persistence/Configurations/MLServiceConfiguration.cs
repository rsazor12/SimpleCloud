using MachineLearning_SimpleCloud_MicroservicesHttp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MachineLearning_SimpleCloud_MicroservicesHttp.Infrastructure.Persistence.Configurations
{
    public class MLServiceConfiguration : IEntityTypeConfiguration<MLService>
    {
        public void Configure(EntityTypeBuilder<MLService> builder)
        {
            builder
                .HasOne(mlService => mlService.ServiceDetails)
                .WithOne(serviceDetails => serviceDetails.MLService)
                .HasForeignKey<MLService>(mlServiceDetails => mlServiceDetails.ServiceDetailsId);

            builder
                .HasOne(mlService => mlService.Client)
                .WithOne(serviceDetails => serviceDetails.MLService)
                .HasForeignKey<MLService>(mlServiceDetails => mlServiceDetails.ClientId);


        }
    }
}
