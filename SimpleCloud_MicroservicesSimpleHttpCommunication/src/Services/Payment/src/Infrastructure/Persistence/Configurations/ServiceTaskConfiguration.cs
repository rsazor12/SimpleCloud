using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleCloud_Monolithic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCloud_Monolithic.Infrastructure.Persistence.Configurations
{
    public class ServiceTaskConfiguration : IEntityTypeConfiguration<ServiceTask>
    {
        public void Configure(EntityTypeBuilder<ServiceTask> builder)
        {
            builder
                .HasOne(serviceTask => serviceTask.ServiceDetails)
                .WithMany(serviceDetails => serviceDetails.ServiceTasks)
                .HasForeignKey(serviceTask => serviceTask.ServiceDetailsId);
        }
    }
}
