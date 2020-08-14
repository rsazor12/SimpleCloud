using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using System;

namespace Payment_SimpleCloud_MicroservicesHttp.WebUI.IntegrationEvents
{
    public class MLTaskPerformedIntegrationEvent : IntegrationEvent
    {
        public string ClientEmail { get; set; }
        public string TaskName { get; set; }
        public DateTime StartTIme { get; set; }
        public DateTime EndTime { get; set; }

        public MLTaskPerformedIntegrationEvent(string clientEmail, string taskName, DateTime startTIme, DateTime endTime)
        {
            ClientEmail = clientEmail;
            TaskName = taskName;
            StartTIme = startTIme;
            EndTime = endTime;
        }
    }
}
