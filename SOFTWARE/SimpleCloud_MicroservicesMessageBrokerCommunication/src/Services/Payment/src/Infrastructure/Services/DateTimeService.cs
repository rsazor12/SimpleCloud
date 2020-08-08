using Payment_SimpleCloud_MicroservicesMessageBroker.Application.Common.Interfaces;
using System;

namespace Payment_SimpleCloud_MicroservicesMessageBroker.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
