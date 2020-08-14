using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace Payment_SimpleCloud_MicroservicesHttp.WebUI.IntegrationEvents
{
    public class ItemCreatedIntegrationEvent : IntegrationEvent
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public ItemCreatedIntegrationEvent(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
