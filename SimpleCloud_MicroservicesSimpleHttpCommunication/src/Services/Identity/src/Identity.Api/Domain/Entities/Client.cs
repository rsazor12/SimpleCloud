using SimpleCloudMonolithic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity_SimpleCloud_MicroservicesHttp.Domain.Entities
{
    public class Client: Entity
    {
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }


        public Client()
        {

        }
        public Client(string email, string password, string name, string surname)
        {
            Password = password;
            Email = email;
            Name = name;
            Surname = surname;
        }
    }
}
