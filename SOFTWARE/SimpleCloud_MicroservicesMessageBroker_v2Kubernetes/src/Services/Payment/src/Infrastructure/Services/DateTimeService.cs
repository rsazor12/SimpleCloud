using Payment_SimpleCloud_MicroservicesHttp.Application.Common.Interfaces;
using System;

namespace Payment_SimpleCloud_MicroservicesHttp.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
