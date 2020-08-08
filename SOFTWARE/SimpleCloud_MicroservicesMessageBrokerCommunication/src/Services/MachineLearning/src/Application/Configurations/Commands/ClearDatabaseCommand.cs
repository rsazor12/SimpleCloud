using MediatR;
using MachineLearning_SimpleCloud_Broker.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MachineLearning_SimpleCloud_Broker.Application.Configurations.Commands
{
    public class ClearDatabaseCommand : IRequest
    {
    }

    public class MakepredictionCommandHandler : IRequestHandler<ClearDatabaseCommand>
    {
        private readonly IMachineLearningDbContext _dbContext;

        public MakepredictionCommandHandler(IMachineLearningDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(ClearDatabaseCommand request, CancellationToken cancellationToken)
        {
            _dbContext.Clients.RemoveRange(_dbContext.Clients);
            _dbContext.ServiceDetails.RemoveRange(_dbContext.ServiceDetails);
            _dbContext.ServiceTasks.RemoveRange(_dbContext.ServiceTasks);
            _dbContext.MLServices.RemoveRange(_dbContext.MLServices);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
