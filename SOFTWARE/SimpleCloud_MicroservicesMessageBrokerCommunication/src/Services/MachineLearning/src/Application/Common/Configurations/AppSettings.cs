using System;
using System.Collections.Generic;
using System.Text;

namespace MachineLearning_SimpleCloud_Broker.Application.Common.Configurations
{
    public class AppSettings
    {
        public FileStorageSettings FileStorageSettings { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
        public string PaymentApi { get; set; }
    }
}
