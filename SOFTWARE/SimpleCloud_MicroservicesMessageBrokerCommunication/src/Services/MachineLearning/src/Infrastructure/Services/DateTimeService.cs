using MachineLearning_SimpleCloud_Broker.Application.Common.Interfaces;
using System;

namespace MachineLearning_SimpleCloud_Broker.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
