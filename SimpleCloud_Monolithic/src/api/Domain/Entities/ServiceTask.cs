using SimpleCloudMonolithic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCloud_Monolithic.Domain.Entities
{
    public class ServiceTask: Entity
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public ServiceTask()
        {

        }
    }
}
