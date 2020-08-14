using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment_SimpleCloud_MicroservicesHttp.WebUI.IntegrationEvents
{
    public class ClientCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid ClientId { get; set; }
        public string Email { get; set; }
    }
}

