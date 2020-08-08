using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment_SimpleCloud_MicroservicesMessageBroker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment_SimpleCloud_MicroservicesMessageBroker.Infrastructure.Persistence.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Domain.Entities.Client>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Client> builder)
        {
            builder
                .HasMany(client => client.ClientTasks)
                .WithOne(clientTask => clientTask.Client);
                    
            //builder
            //    .HasOne(serviceTask => serviceTask.ServiceDetails)
            //    .WithMany(serviceDetails => serviceDetails.ServiceTasks)
            //    .HasForeignKey(serviceTask => serviceTask.ServiceDetailsId);
        }
    }
}
