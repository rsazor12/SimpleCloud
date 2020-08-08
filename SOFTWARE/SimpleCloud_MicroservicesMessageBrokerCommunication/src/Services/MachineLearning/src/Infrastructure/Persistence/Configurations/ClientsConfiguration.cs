using MachineLearning_SimpleCloud_Broker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MachineLearning_SimpleCloud_Broker.Infrastructure.Persistence.Configurations
{
    public class ClientsConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
           
        }
    }
}
