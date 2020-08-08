using MachineLearning_SimpleCloud_Broker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MachineLearning_SimpleCloud_Broker.Domain.Entities
{
    public class ServiceDetails: Entity
    {
        public ICollection<ServiceTask> ServiceTasks { get; private set; }
        public string TrainDataPath { get; set; }
        public string TestDataPath { get; set; }
        public string ModelPath { get; set; }
        public int Epochs { get; private set; }
        public int BatchSize { get; private set; }
        public double LearningRate { get; private set; }


        // EF Core
        // public Guid MLServiceId { get; set; }
        public MLService MLService { get; set; }

        public ServiceDetails()
        {
            ServiceTasks = new HashSet<ServiceTask>();
        }

        public ServiceDetails(
            string trainDataPath, string testDataPath, string modelPath, int epochs, int batchSize, double learningRate)
        {
            ServiceTasks = new HashSet<ServiceTask>();
            TrainDataPath = trainDataPath;
            TestDataPath = testDataPath;
            ModelPath = modelPath;
            Epochs = epochs;
            BatchSize = batchSize;
            LearningRate = learningRate;
        }

        public void UpdateLearningParameters(int epochs, int batchSize, double learningRate)
        {
            Epochs = epochs;
            BatchSize = batchSize;
            LearningRate = learningRate;
        }

        public void AddTask(string name, DateTime startTime, DateTime endTime)
        {
            ServiceTasks.Add(new ServiceTask(name, startTime, endTime));
        }
        public void AddTask(ServiceTask task)
        {
            ServiceTasks.Add(task);
        }
    }
}
