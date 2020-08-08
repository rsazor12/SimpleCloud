using System;
using System.Collections.Generic;
using System.Text;

namespace Payment_SimpleCloud_MicroservicesMessageBroker.Application.Invoice.Queries.GetInvoice
{
    public class GetInvoiceVM
    {
        public string TaskName { get; set; }
        public double Time { get; set; }
        public double MilisecondRate { get; set; }
        public double Cost { get; set; }

        public GetInvoiceVM(string taskName, double time, double milisecondRate, double cost)
        {
            TaskName = taskName;
            Time = time;
            MilisecondRate = milisecondRate;
            Cost = cost;
        }
    }
}
