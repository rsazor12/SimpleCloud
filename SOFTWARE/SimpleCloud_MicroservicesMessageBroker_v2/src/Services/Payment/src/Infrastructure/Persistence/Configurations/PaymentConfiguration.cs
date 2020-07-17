using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment_SimpleCloud_MicroservicesHttp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment_SimpleCloud_MicroservicesHttp.Infrastructure.Persistence.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            //builder
            //    .h
            //    .HasMany(client => client.ClientTasks)
            //    .WithOne(clientTask => clientTask.Client);
                    
            //builder
            //    .HasOne(serviceTask => serviceTask.ServiceDetails)
            //    .WithMany(serviceDetails => serviceDetails.ServiceTasks)
            //    .HasForeignKey(serviceTask => serviceTask.ServiceDetailsId);
        }
    }
}
