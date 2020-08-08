using System;
using System.Collections.Generic;
using System.Text;

namespace Payment_SimpleCloud_MicroservicesMessageBroker.Application.Common.Configurations
{
    public class AppSettings
    {
        public FileStorageSettings FileStorageSettings { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
    }
}
