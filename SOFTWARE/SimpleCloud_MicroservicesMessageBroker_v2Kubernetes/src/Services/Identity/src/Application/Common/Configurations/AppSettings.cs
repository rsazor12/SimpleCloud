using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCloud_Monolithic.Application.Common.Configurations
{
    public class AppSettings
    {
        public FileStorageSettings FileStorageSettings { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
    }
}
