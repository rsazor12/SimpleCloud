using Payment_SimpleCloud_MicroservicesHttp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment_SimpleCloud_MicroservicesHttp.Domain.Entities
{
    public class ClientTask: Entity
    {
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        // EF Core
        // public Guid MLServiceId { get; set; }
        public Guid ServiceDetailsId { get; private set; }
        // public ServiceDetails ServiceDetails { get; set; }


        public ClientTask()
        {

        }

        public ClientTask(string name, DateTime startTime, DateTime endTime)
        {
            Name = name;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
