using System;
using System.Collections.Generic;
using System.Text;

namespace Identity_SimpleCloud_MicroservicesHttp.Application.Common.Configurations
{
    public class RabbitMqSettings
    {
      public string BrokerName { get; set; }
      public string AutofacScopeName { get; set; }
      public string QueueName { get; set; }
      public string RetryCount { get; set; }
      public string VirtualHost { get; set; }
      public string Username { get; set; }
      public string Password { get; set; }
      public string Host { get; set; }
      public string DispatchConsumersAsync { get; set; }
    }
}
