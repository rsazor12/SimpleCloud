using Microsoft.EntityFrameworkCore;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using System.Threading.Tasks;
using System.Threading;
using Payment_SimpleCloud_MicroservicesHttp.Application.Common.Interfaces;
using Payment_SimpleCloud_MicroservicesHttp.Domain.Entities;
using Payment_SimpleCloud_MicroservicesHttp.WebUI.IntegrationEvents;

namespace Payment_SimpleCloud_MicroservicesHttp.Application.IntegrationEvents.EventHandling
{
    public class ClientCreatedIntegrationEventHandler : IIntegrationEventHandler<ClientCreatedIntegrationEvent>
    {
        private readonly IPaymentDbContext _paymentDbContext;

        public ClientCreatedIntegrationEventHandler(IPaymentDbContext dbContext)
        {
            _paymentDbContext = dbContext;
        }

        public async Task Handle(ClientCreatedIntegrationEvent @event)
        {
            var user = await _paymentDbContext.Clients
                .FirstOrDefaultAsync(client => client.Email == @event.Email);

            if (user != null)
                return;

            var newUser = new Client(@event.ClientId, @event.Email);

            await _paymentDbContext.Clients.AddAsync(newUser);

            await _paymentDbContext.SaveChangesAsync(default(CancellationToken));

        }
    }

}
