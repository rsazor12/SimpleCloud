using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Identity_SimpleCloud_MicroservicesHttp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity_SimpleCloud_MicroservicesHttp.Infrastructure.Persistence.Configurations
{
    public class MLServiceConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            //builder
            //    .HasOne(mlService => mlService.ServiceDetails)
            //    .WithOne(serviceDetails => serviceDetails.MLService)
            //    .HasForeignKey<MLService>(mlServiceDetails => mlServiceDetails.ServiceDetailsId);

            //builder
            //    .HasOne(mlService => mlService.Client)
            //    .WithOne(serviceDetails => serviceDetails.MLService)
            //    .HasForeignKey<MLService>(mlServiceDetails => mlServiceDetails.ClientId);


        }
    }
}
