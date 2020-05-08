using AutoMapper;
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
using SimpleCloudMonolithic.Application.Common.Mappings;

namespace SimpleCloud_Monolithic.Application.MLService.Commands.UpdateMLService
{
    public class UpdateMLServiceCommand : IRequest, IMapFrom<OrderedMLService>
    {
        public Guid OrderedServiceId { get; set; }
        public string ServiceName { get; set; }
        public Guid CliendId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateMLServiceCommand, OrderedMLService>()
                .ForMember(orderedMLService => orderedMLService.Id, opt => opt.Ignore());
        }
    }

    public class UpdateMLServiceCommandHandler : IRequestHandler<UpdateMLServiceCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateMLServiceCommandHandler(
            IApplicationDbContext dbContext, 
            IMapper mapper
            )
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateMLServiceCommand request, CancellationToken cancellationToken)
        {
            var orderedMLService =
               _dbContext.OrderedServices.SingleOrDefault(orderedService => orderedService.Id == request.OrderedServiceId)
               ?? throw new NotFoundException(nameof(OrderedMLService), request.OrderedServiceId);

            _mapper.Map(request, orderedMLService);

            _dbContext.OrderedServices.Update(orderedMLService);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

    }
}
