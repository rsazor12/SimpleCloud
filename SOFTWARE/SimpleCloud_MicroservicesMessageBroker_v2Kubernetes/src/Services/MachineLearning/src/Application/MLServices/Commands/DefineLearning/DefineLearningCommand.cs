using MachineLearning_SimpleCloud_MicroservicesHttp.Domain.Entities;
using MediatR;
using MachineLearning_SimpleCloud_MicroservicesHttp.Application.Common.Exceptions;
using MachineLearning_SimpleCloud_MicroservicesHttp.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MachineLearning_SimpleCloud_MicroservicesHttp.Application.MLServices.Commands.DefineLearning
{
    public class DefineLearningCommand : IRequest
    {
        public Guid OrderedServiceId { get; set; }

        public int Epochs { get; set; }
        public int BatchSize { get; set; }
        public double LearningRate { get; set; }
        //public string TrainDataPath { get; set; }
        //public string TestDataPath { get; set; }
        //public string ModelName { get; set; }

    }

    public class DefineLearningCommandCommandHandler : IRequestHandler<DefineLearningCommand>
    {
        private readonly IMachineLearningDbContext _dbContext;

        public DefineLearningCommandCommandHandler(IMachineLearningDbContext context)
        {
            _dbContext = context;
        }

        public async Task<Unit> Handle(DefineLearningCommand request, CancellationToken cancellationToken)
        {
            var orderedService =
                _dbContext.MLServices.SingleOrDefault(orderedService => orderedService.Id == request.OrderedServiceId)
                ?? throw new NotFoundException(nameof(Domain.Entities.MLService), request.OrderedServiceId);

            var serviceDetails = new ServiceDetails();

            serviceDetails.UpdateLearningParameters(request.Epochs, request.BatchSize, request.LearningRate);

            orderedService.UpdateServiceDetails(serviceDetails);

            _dbContext.MLServices.Update(orderedService);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
