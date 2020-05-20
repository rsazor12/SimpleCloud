using MediatR;
using SimpleCloud_Monolithic.Domain.Entities;
using SimpleCloudMonolithic.Application.Common.Exceptions;
using SimpleCloudMonolithic.Application.Common.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleCloud_Monolithic.Application.MLServices.Commands.CreateLearningService
{
    public class CreateMLServiceCommand : IRequest<Guid>
    {
        public string ServiceName { get; set; }
        public Guid CliendId { get; set; }
    }

    public class CreateMLServiceCommandHandler : IRequestHandler<CreateMLServiceCommand, Guid>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateMLServiceCommandHandler(IApplicationDbContext context)
        {
            _dbContext = context;
        }
        public async Task<Guid> Handle(CreateMLServiceCommand request, CancellationToken cancellationToken)
        {
            var client =
                _dbContext.Clients.SingleOrDefault(client => client.Id == request.CliendId)
                ?? throw new NotFoundException(nameof(Client), request.CliendId);

            var orderedService = new Domain.Entities.MLService();

            orderedService.UpdateServiceName(request.ServiceName);
            orderedService.UpdateServiceDetails(new ServiceDetails());
            orderedService.AssignClient(client);

            _dbContext.MLServices.Add(orderedService);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return orderedService.Id;
        }
    }
}
