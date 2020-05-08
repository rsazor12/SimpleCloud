using SimpleCloud_Monolithic.Domain.Entities;
using MediatR;
using SimpleCloudMonolithic.Application.Common.Exceptions;
using SimpleCloudMonolithic.Application.Common.Interfaces;
using SimpleCloudMonolithic.Application.MachineLearning.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleCloud_Monolithic.Application.MLService.Commands.CreateLearningService
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

            var orderedService = new OrderedMLService(request.ServiceName, null, null);

            _dbContext.OrderedServices.Add(orderedService);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return orderedService.Id;
        }
    }
}
