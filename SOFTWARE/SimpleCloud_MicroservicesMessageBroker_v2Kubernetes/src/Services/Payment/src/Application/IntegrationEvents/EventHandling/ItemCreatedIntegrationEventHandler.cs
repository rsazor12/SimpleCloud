using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Payment_SimpleCloud_MicroservicesHttp.WebUI.IntegrationEvents;
using System.Threading.Tasks;

namespace Payment_SimpleCloud_MicroservicesHttp.Application.IntegrationEvents.EventHandling
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
