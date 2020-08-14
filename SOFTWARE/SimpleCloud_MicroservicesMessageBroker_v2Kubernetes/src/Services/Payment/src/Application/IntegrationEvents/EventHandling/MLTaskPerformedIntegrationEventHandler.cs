using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Payment_SimpleCloud_MicroservicesHttp.Application.ClientTasks.Commands;
using Payment_SimpleCloud_MicroservicesHttp.Application.Common.Interfaces;
using Payment_SimpleCloud_MicroservicesHttp.WebUI.IntegrationEvents;
using System.Threading;
using System.Threading.Tasks;

namespace Payment_SimpleCloud_MicroservicesHttp.Application.IntegrationEvents.EventHandling
{
    public class MLTaskPerformedIntegrationEventHandler : IIntegrationEventHandler<MLTaskPerformedIntegrationEvent>
    {
        private readonly IPaymentDbContext _dbContext;


        public MLTaskPerformedIntegrationEventHandler(IPaymentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public MLTaskPerformedIntegrationEventHandler()
        {
        }

        public async Task Handle(MLTaskPerformedIntegrationEvent @event)
        {
            var addClientTaskCommand = new AddClientTaskCommand() {
                ClientEmail = @event.ClientEmail,
                TaskName = @event.TaskName,
                StartTIme = @event.StartTIme,
                EndTime = @event.EndTime          
            };
            var handler = new AddClientTaskCommandHandler(_dbContext);


            await handler.Handle(addClientTaskCommand, default(CancellationToken));
        }
    }
}
