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
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using SimpleCloud_Monolithic.Application.Models.HandlerResponse;
//using Microsoft.EntityFrameworkCore.ChangeTracking;
//using Microsoft.EntityFrameworkCore;

namespace SimpleCloud_Monolithic.Application.MLServices.Commands.UpdateMLService
{
    public class UpdateMLServiceCommand : IRequest<CommandHandlerResponse>, IMapFrom<MLService>
    {
        public Guid OrderedServiceId { get; set; }
        public string ServiceName { get; set; }
        public Guid CliendId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateMLServiceCommand, MLService>()
                .ForMember(orderedMLService => orderedMLService.Id, opt => opt.Ignore());

            profile.CreateMap<ServiceDetails, ServiceDetails>()
                .ForMember(orderedMLService => orderedMLService.Id, opt => opt.Ignore());
        }
    }

    public class UpdateMLServiceCommandHandler : IRequestHandler<UpdateMLServiceCommand, CommandHandlerResponse>
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
        public async Task<CommandHandlerResponse> Handle(UpdateMLServiceCommand request, CancellationToken cancellationToken)
        {
            var orderedMLService =
               _dbContext.MLServices.Include(mlService => mlService.ServiceDetails).SingleOrDefault(orderedService => orderedService.Id == request.OrderedServiceId)
               ?? throw new NotFoundException(nameof(MLService), request.OrderedServiceId);

            _mapper.Map(request, orderedMLService);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CommandHandlerResponse();
        }

    }
}
