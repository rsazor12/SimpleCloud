using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleCloud_Monolithic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCloud_Monolithic.Infrastructure.Persistence.Configurations
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
