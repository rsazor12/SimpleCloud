using SimpleCloudMonolithic.Application.Common.Interfaces;
using System;

namespace SimpleCloudMonolithic.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
