using MediatR;
using MachineLearning_SimpleCloud_MicroservicesHttp.Application.Models.HandlerResponse;
using MachineLearning_SimpleCloud_MicroservicesHttp.Domain.Entities;
using MachineLearning_SimpleCloud_MicroservicesHttp.Application.Common.Exceptions;
using MachineLearning_SimpleCloud_MicroservicesHttp.Application.Common.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using MachineLearning_SimpleCloud_MicroservicesHttp.WebUI.IntegrationEvents;

namespace MachineLearning_SimpleCloud_MicroservicesHttp.Application.MLServices.Commands.CreateLearningService
{
    public class CreateMLServiceCommand : IRequest<CommandHandlerResponse<Guid>>
    {
        public string ServiceName { get; set; }
        public Guid ClientId { get; set; }
    }

    public class CreateMLServiceCommandHandler : IRequestHandler<CreateMLServiceCommand, CommandHandlerResponse<Guid>>
    {
        private readonly IMachineLearningDbContext _dbContext;
        private readonly IEventBus _eventBus;

        public CreateMLServiceCommandHandler(IMachineLearningDbContext context, IEventBus eventBus)
        {
            _dbContext = context;
            _eventBus = eventBus;
        }
        public async Task<CommandHandlerResponse<Guid>> Handle(CreateMLServiceCommand request, CancellationToken cancellationToken)
        {
            //DateTime startTime = DateTime.UtcNow;
            //DateTime endTime;

            var client =
                _dbContext.Clients.SingleOrDefault(client => client.Id == request.ClientId)
                ?? throw new NotFoundException(nameof(Client), request.ClientId);

            var orderedService = new Domain.Entities.MLService();

            orderedService.UpdateServiceName(request.ServiceName);
            orderedService.UpdateServiceDetails(new ServiceDetails());
            orderedService.AssignClient(client);

            _dbContext.MLServices.Add(orderedService);

            await _dbContext.SaveChangesAsync(cancellationToken);

            //endTime = DateTime.UtcNow;

            //var @event = new MLTaskPerformedIntegrationEvent() { 
            //    ClientEmail = client.Email,
            //    StartTime = startTime,
            //    EndTime = endTime,
            //    TaskName = GetType().Name
            //};

            //_eventBus.Publish(@event);

            return new CommandHandlerResponse<Guid>() { Response = orderedService.Id };
        }
    }
}
