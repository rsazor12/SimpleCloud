using AutoMapper;
using MachineLearning_SimpleCloud_Broker.Domain.Entities;
using MediatR;
using MachineLearning_SimpleCloud_Broker.Application.Common.Exceptions;
using MachineLearning_SimpleCloud_Broker.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MachineLearning_SimpleCloud_Broker.Application.Common.Mappings;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using MachineLearning_SimpleCloud_Broker.Application.Models.HandlerResponse;
//using Microsoft.EntityFrameworkCore.ChangeTracking;
//using Microsoft.EntityFrameworkCore;

namespace MachineLearning_SimpleCloud_Broker.Application.MLServices.Commands.UpdateMLService
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
        private readonly IMachineLearningDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateMLServiceCommandHandler(
            IMachineLearningDbContext dbContext, 
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
