﻿using Payment_SimpleCloud_MicroservicesHttp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment_SimpleCloud_MicroservicesHttp.Domain.Entities
{
    public class Payment: Entity
    {
        public PaymentStatus Status { get; private set; }
        public DateTime EndTime { get; set; }

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