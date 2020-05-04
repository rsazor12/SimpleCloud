using ca_sln.Application.Common.Interfaces;
using System;

namespace ca_sln.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
