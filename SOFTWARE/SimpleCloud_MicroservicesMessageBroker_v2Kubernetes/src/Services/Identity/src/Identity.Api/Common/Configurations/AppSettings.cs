// using EventBus.RabbitMQ.Standard.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity_SimpleCloud_MicroservicesHttp.Application.Common.Configurations
{
    public class AppSettings
    {
        public FileStorageSettings FileStorageSettings { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
        // public RabbitMqOptions RabbitMq { get; set; }
        public string MachineLearningApi { get; set; }
        public string PaymentApi { get; set; }
        public string SubscriptionClientName { get; set; }
        public string EventBusRetryCount { get; set; }
        public string EventBusConnection { get; set; }
        public string EventBusUserName { get; set; }
        public string EventBusPassword { get; set; }
        public string EventBusHostName { get; set; }

    }
}
