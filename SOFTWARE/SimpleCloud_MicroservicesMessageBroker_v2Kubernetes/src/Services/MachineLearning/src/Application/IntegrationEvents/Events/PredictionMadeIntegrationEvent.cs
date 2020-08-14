using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace MachineLearning_SimpleCloud_MicroservicesHttp.WebUI.IntegrationEvents
{
    public class PredictionMadeIntegrationEvent : IntegrationEvent
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public PredictionMadeIntegrationEvent(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
