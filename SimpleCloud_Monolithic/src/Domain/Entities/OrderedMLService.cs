using SimpleCloudMonolithic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCloud_Monolithic.Domain.Entities
{
    public class OrderedMLService: Entity
    {
        public string ServiceName { get; private set; }
        public Client Client { get; private set; }
        public ServiceDetails ServiceDetails { get; private set; }

        public OrderedMLService()
        {

        }

        public OrderedMLService(
            string serviceName,
            Client client,
            ServiceDetails serviceDetails
            )
        {
            ServiceName = serviceName;
            Client = client;
            ServiceDetails = serviceDetails;
        }

        public void UpdateServiceDetails(ServiceDetails serviceDetails)
        {
            ServiceDetails = serviceDetails;
        }

        public void UploadFiles()
        {

        }
    }
}
