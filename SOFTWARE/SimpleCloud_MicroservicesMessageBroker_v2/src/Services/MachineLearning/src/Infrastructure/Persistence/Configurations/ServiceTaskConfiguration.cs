using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MachineLearning_SimpleCloud_MicroservicesHttp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MachineLearning_SimpleCloud_MicroservicesHttp.Infrastructure.Persistence.Configurations
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
