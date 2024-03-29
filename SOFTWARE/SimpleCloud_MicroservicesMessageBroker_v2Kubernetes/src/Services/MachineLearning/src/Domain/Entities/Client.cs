﻿using MachineLearning_SimpleCloud_MicroservicesHttp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MachineLearning_SimpleCloud_MicroservicesHttp.Domain.Entities
{
    public class Client: Entity
    {
        public string Email { get; private set; }
        //public string Name { get; private set; }
        //public string Surname { get; private set; }

        public MLService MLService { get; private set; }

        public Client()
        {

        }
        public Client(Guid id, string email)
        {
            Id = id;
            Email = email;
        }
    }
}
