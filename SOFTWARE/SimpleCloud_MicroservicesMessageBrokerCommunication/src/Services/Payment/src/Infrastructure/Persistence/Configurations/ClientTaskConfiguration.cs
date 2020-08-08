using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment_SimpleCloud_MicroservicesMessageBroker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment_SimpleCloud_MicroservicesMessageBroker.Infrastructure.Persistence.Configurations
{
    public class ClientTaskConfiguration : IEntityTypeConfiguration<Domain.Entities.ClientTask>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.ClientTask> builder)
        {
            //builder
            //    .HasOne(serviceTask => serviceTask.ServiceDetails)
            //    .WithMany(serviceDetails => serviceDetails.ServiceTasks)
            //    .HasForeignKey(serviceTask => serviceTask.ServiceDetailsId);
        }
    }
}
