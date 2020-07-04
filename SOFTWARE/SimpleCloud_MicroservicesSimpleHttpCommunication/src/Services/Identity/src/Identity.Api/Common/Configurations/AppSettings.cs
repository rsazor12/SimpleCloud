using System;
using System.Collections.Generic;
using System.Text;

namespace Identity_SimpleCloud_MicroservicesHttp.Application.Common.Configurations
{
    public class AppSettings
    {
        public FileStorageSettings FileStorageSettings { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
        public string MachineLearningApi { get; set; }
    }
}
