using SimpleCloudMonolithic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCloud_Monolithic.Domain.Entities
{
    public class Client: Entity
    {
        public string Email { get; set; }

        public Client()
        {

        }
    }
}
