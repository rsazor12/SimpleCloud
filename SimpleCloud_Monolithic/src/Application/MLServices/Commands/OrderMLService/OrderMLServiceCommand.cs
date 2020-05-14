using MediatR;
using SimpleCloud_Monolithic.Domain.Entities;
using SimpleCloudMonolithic.Application.Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleCloud_Monolithic.Application.MLServices.Commands.CreateLearningService
{
    public class OrderMLServiceCommand : IRequest<Guid>
    {
        public string ServiceName { get; set; }
        public Guid CliendId { get; set; }
    }

    public class OrderMLServiceCommandHandler : IRequestHandler<OrderMLServiceCommand, Guid>
    {
        private readonly IApplicationDbContext _dbContext;

        public OrderMLServiceCommandHandler(IApplicationDbContext context)
        {
            _dbContext = context;
        }
        public async Task<Guid> Handle(OrderMLServiceCommand request, CancellationToken cancellationToken)
        {
            //var client = 
            //    _dbContext.Clients.SingleOrDefault(client => client.Id == request.CliendId)
            //    ?? throw new NotFoundException(nameof(Client), request.CliendId);

            var orderedService = new Domain.Entities.MLService();

            orderedService.UpdateServiceName(request.ServiceName);
            orderedService.UpdateServiceDetails(new ServiceDetails());
            orderedService.AssignClient(new Client());

            _dbContext.MLServices.Add(orderedService);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return orderedService.Id;
        }
    }
}
