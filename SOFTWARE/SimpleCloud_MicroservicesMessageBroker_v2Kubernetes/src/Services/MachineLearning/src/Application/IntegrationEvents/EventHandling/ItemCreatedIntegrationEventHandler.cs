using MachineLearning_SimpleCloud_MicroservicesHttp.WebUI.IntegrationEvents;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using System.Threading.Tasks;

namespace MachineLearning_SimpleCloud_MicroservicesHttp.Application.IntegrationEvents.EventHandling
{
    public class ItemCreatedIntegrationEventHandler : IIntegrationEventHandler<ItemCreatedIntegrationEvent>
    {
        public ItemCreatedIntegrationEventHandler()
        {
        }

        public async Task Handle(ItemCreatedIntegrationEvent @event)
        {
            //Handle the ItemCreatedIntegrationEvent event here.
        }
    }
}
