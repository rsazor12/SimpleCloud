using SimpleCloudMonolithic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCloud_Monolithic.Domain.Entities
{
    public class ServiceDetails: Entity
    {
        public ICollection<ServiceTask> ServiceTasks { get; private set; }
        public string TrainDataPath { get; private set; }
        public string TestDataPath { get; private set; }
        public string ModelPath { get; private set; }
        public int Epochs { get; private set; }
        public int BatchSize { get; private set; }
        public double LearningRate { get; private set; }

        public ServiceDetails()
        {

        }

        public ServiceDetails(
            string trainDataPath, string testDataPath, string modelPath, int epochs, int batchSize, double learningRate)
        {
            ServiceTasks = ServiceTasks = new HashSet<ServiceTask>();
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
    }
}
