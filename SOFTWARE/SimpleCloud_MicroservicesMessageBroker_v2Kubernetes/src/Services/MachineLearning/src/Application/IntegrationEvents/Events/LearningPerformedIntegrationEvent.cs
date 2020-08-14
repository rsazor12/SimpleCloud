using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace MachineLearning_SimpleCloud_MicroservicesHttp.WebUI.IntegrationEvents
{
    public class LearningPerformedIntegrationEvent : IntegrationEvent
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public LearningPerformedIntegrationEvent(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
