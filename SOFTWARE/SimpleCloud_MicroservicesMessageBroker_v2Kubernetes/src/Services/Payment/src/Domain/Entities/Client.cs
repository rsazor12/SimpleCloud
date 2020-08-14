using Payment_SimpleCloud_MicroservicesHttp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment_SimpleCloud_MicroservicesHttp.Domain.Entities
{
    public class Client: Entity
    {
        public string Email { get; private set; }

        public ICollection<ClientTask> ClientTasks { get; private set; }

        public Client()
        {
            ClientTasks = new HashSet<ClientTask>();
        }
        public Client(Guid id, string email)
        {
            ClientTasks = new HashSet<ClientTask>();
            Id = id;
            Email = email;
        }

        public void AddTask(ClientTask task)
        {
            ClientTasks.Add(task);
        }
    }
}
