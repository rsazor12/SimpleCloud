using Identity_SimpleCloud_MicroservicesMessageBroker.Application.Common.Interfaces;
using System;

namespace Identity_SimpleCloud_MicroservicesMessageBroker.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
