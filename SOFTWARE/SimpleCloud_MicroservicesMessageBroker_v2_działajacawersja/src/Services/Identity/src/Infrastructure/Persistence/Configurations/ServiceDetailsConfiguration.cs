using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleCloud_Monolithic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCloud_Monolithic.Infrastructure.Persistence.Configurations
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
