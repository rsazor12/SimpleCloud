using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using System;

namespace MachineLearning_SimpleCloud_MicroservicesHttp.WebUI.IntegrationEvents
{
    public class MLTaskPerformedIntegrationEvent : IntegrationEvent
    {
        public string ClientEmail { get; set; }
        public string TaskName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public MLTaskPerformedIntegrationEvent()
        {
                
        }
        public MLTaskPerformedIntegrationEvent(string clientEmail, string taskName, DateTime startTIme, DateTime endTime)
        {
            ClientEmail = clientEmail;
            TaskName = taskName;
            StartTime = startTIme;
            EndTime = endTime;
        }
    }
}
