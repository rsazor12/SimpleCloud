using Identity_SimpleCloud_MicroservicesHttp.Application.Common.Interfaces;
using System;

namespace Identity_SimpleCloud_MicroservicesHttp.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
