using SimpleCloudMonolithic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCloud_Monolithic.Domain.Entities
{
    public class Client: Entity
    {
        public string Email { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }

        public MLService MLService { get; private set; }

        public Client()
        {

        }
        public Client(string email, string name, string surname)
        {
            Email = email;
            Name = name;
            Surname = surname;
        }
    }
}
