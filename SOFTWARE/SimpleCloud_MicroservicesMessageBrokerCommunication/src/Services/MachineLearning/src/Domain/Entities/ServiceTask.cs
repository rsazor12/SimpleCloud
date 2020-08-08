using MachineLearning_SimpleCloud_Broker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MachineLearning_SimpleCloud_Broker.Domain.Entities
{
    public class ServiceTask: Entity
    {
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        // EF Core
        // public Guid MLServiceId { get; set; }
        public Guid ServiceDetailsId { get; private set; }
        public ServiceDetails ServiceDetails { get; set; }


        public ServiceTask()
        {

        }

        public ServiceTask(string name, DateTime startTime, DateTime endTime)
        {
            Name = name;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
