using Payment_SimpleCloud_MicroservicesMessageBroker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment_SimpleCloud_MicroservicesMessageBroker.Domain.Entities
{
    public class Payment: Entity
    {
        public PaymentStatus Status { get; private set; }
        public DateTime Date { get; set; }

        public double Amount { get; set; }


        public Payment()
        {
            Status = PaymentStatus.UNPAID;
        }

        public void SwitchStatus(PaymentStatus status)
        {
            Status = status;
        }
    }
}
