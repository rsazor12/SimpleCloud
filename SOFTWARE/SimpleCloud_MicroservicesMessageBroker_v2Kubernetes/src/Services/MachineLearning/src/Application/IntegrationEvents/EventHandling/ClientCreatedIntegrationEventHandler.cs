using MachineLearning_SimpleCloud_MicroservicesHttp.Application.Common.Interfaces;
using MachineLearning_SimpleCloud_MicroservicesHttp.WebUI.IntegrationEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using System.Threading.Tasks;
using MachineLearning_SimpleCloud_MicroservicesHttp.Application.Common.Exceptions;
using MachineLearning_SimpleCloud_MicroservicesHttp.Domain.Entities;
using System.Threading;

namespace MachineLearning_SimpleCloud_MicroservicesHttp.Application.IntegrationEvents.EventHandling
{
    public class ClientCreatedIntegrationEventHandler : IIntegrationEventHandler<ClientCreatedIntegrationEvent>
    {
        private readonly IMachineLearningDbContext _machineLearningDbContext;

        public ClientCreatedIntegrationEventHandler(IMachineLearningDbContext dbContext)
        {
            _machineLearningDbContext = dbContext;
        }

        public async Task Handle(ClientCreatedIntegrationEvent @event)
        {
            var user = await _machineLearningDbContext.Clients
                .FirstOrDefaultAsync(client => client.Email == @event.Email);

            if (user != null)
                return;

            //if (user != null)
            //    throw new ConflictException($"Entity {nameof(user)} with email {command.Email} already exists in database");

            var newUser = new Client(@event.ClientId, @event.Email);

            await _machineLearningDbContext.Clients.AddAsync(newUser);

            await _machineLearningDbContext.SaveChangesAsync(default(CancellationToken));

        }
    }

}
