using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace MachineLearning_SimpleCloud_MicroservicesHttp.WebUI.IntegrationEvents
{
    public class LearningFilesUploadedIntegrationEvent : IntegrationEvent
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public LearningFilesUploadedIntegrationEvent(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
