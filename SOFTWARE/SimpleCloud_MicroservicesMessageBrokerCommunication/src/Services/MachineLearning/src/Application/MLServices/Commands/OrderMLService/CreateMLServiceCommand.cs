using MediatR;
using MachineLearning_SimpleCloud_Broker.Application.Models.HandlerResponse;
using MachineLearning_SimpleCloud_Broker.Domain.Entities;
using MachineLearning_SimpleCloud_Broker.Application.Common.Exceptions;
using MachineLearning_SimpleCloud_Broker.Application.Common.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MachineLearning_SimpleCloud_Broker.Application.MLServices.Commands.CreateLearningService
{
    public class CreateMLServiceCommand : IRequest<CommandHandlerResponse<Guid>>
    {
        public string ServiceName { get; set; }
        public Guid ClientId { get; set; }
    }

    public class CreateMLServiceCommandHandler : IRequestHandler<CreateMLServiceCommand, CommandHandlerResponse<Guid>>
    {
        private readonly IMachineLearningDbContext _dbContext;

        public CreateMLServiceCommandHandler(IMachineLearningDbContext context)
        {
            _dbContext = context;
        }
        public async Task<CommandHandlerResponse<Guid>> Handle(CreateMLServiceCommand request, CancellationToken cancellationToken)
        {
            var client =
                _dbContext.Clients.SingleOrDefault(client => client.Id == request.ClientId)
                ?? throw new NotFoundException(nameof(Client), request.ClientId);

            var orderedService = new Domain.Entities.MLService();

            orderedService.UpdateServiceName(request.ServiceName);
            orderedService.UpdateServiceDetails(new ServiceDetails());
            orderedService.AssignClient(client);

            _dbContext.MLServices.Add(orderedService);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CommandHandlerResponse<Guid>() { Response = orderedService.Id };
        }
    }
}
