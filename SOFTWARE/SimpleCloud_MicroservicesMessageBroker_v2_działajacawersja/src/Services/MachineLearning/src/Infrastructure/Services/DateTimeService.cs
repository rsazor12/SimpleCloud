using MachineLearning_SimpleCloud_MicroservicesHttp.Application.Common.Interfaces;
using System;

namespace MachineLearning_SimpleCloud_MicroservicesHttp.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
