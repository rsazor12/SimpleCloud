using SimpleCloud_Monolithic.Domain.Entities;
using MediatR;
using SimpleCloudMonolithic.Application.Common.Exceptions;
using SimpleCloudMonolithic.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleCloud_Monolithic.Application.MLService.Commands.UploadLearningFiles
{
    public class UploadLearningFilesCommand : IRequest
    {
        public Guid OrderedServiceId { get; set; }
    }

    public class OrderServiceCommandHandler : IRequestHandler<UploadLearningFilesCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public OrderServiceCommandHandler(IApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task<Unit> Handle(UploadLearningFilesCommand request, CancellationToken cancellationToken)
        {
            var orderedService =
                _dbContext.OrderedServices.SingleOrDefault(orderedService => orderedService.Id == request.OrderedServiceId)
                ?? throw new NotFoundException(nameof(OrderedMLService), request.OrderedServiceId);

            return Unit.Value;

        }

        //Task<Unit> Handle(UploadLearningFilesCommand request, CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
