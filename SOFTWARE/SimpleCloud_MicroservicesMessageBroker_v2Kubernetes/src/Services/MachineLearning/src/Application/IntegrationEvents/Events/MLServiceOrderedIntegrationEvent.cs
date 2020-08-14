using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace MachineLearning_SimpleCloud_MicroservicesHttp.WebUI.IntegrationEvents
{
    public class MLServiceOrderedIntegrationEvent : IntegrationEvent
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public MLServiceOrderedIntegrationEvent(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
