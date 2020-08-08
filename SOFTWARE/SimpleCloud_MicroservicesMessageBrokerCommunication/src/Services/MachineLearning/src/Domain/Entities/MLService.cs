using MachineLearning_SimpleCloud_Broker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MachineLearning_SimpleCloud_Broker.Domain.Entities
{
    public class MLService: Entity
    {
        public string ServiceName { get; private set; }

        public Guid ClientId { get; private set; }
        public Client Client { get; private set; }

        public Guid ServiceDetailsId { get; private set; }
        public ServiceDetails ServiceDetails { get; private set; }

        public MLService()
        {
            // ServiceDetails = new ServiceDetails();
        }

        public void UpdateServiceName(string serviceName)
        {
            ServiceName = serviceName;
        }

        public void UpdateServiceDetails(ServiceDetails serviceDetails)
        {
            ServiceDetails = serviceDetails;
        }

        public void UpdateLearningFilesPath(string learningPath)
        {
           ServiceDetails.TrainDataPath = learningPath;
        }

        public void UpdateTestingFilesPath(string testPath)
        {
            ServiceDetails.TestDataPath = testPath;
        }

        public void UpdateModelPath(string modelPath)
        {
            ServiceDetails.ModelPath = modelPath;
        }

        public void AssignClient(Client client)
        {
            Client = client;
        }

        public void AddTask(string name, DateTime startTime, DateTime endTime)
        {
            ServiceDetails.AddTask(name, startTime, endTime);
        }

        public void AddTask(ServiceTask task)
        {
            ServiceDetails.AddTask(task);
        }
    }
}
