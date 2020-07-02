using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment_SimpleCloud_MicroservicesHttp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment_SimpleCloud_MicroservicesHttp.Infrastructure.Persistence.Configurations
{
    public class ServiceTaskConfiguration : IEntityTypeConfiguration<ClientTask>
    {
        public void Configure(EntityTypeBuilder<ClientTask> builder)
        {
            //builder
            //    .HasOne(serviceTask => serviceTask.ServiceDetails)
            //    .WithMany(serviceDetails => serviceDetails.ServiceTasks)
            //    .HasForeignKey(serviceTask => serviceTask.ServiceDetailsId);
        }
    }
}
