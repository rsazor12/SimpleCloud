using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleCloud_Monolithic.Application.Common.Exceptions;
using SimpleCloud_Monolithic.Application.Models.HandlerResponse;
using SimpleCloud_Monolithic.Domain.Entities;
using SimpleCloudMonolithic.Application.Common.Interfaces;
using SimpleCloudMonolithic.Application.Common.Mappings;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleCloud_Monolithic.Application.Clients.Commands.CreateClient
{
    public class CreateClientCommand : IRequest<CommandHandlerResponse<Guid>>, IMapFrom<Client>
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateClientCommand, Client>()
                .ForMember(user => user.Id, opt => opt.Ignore());
        }
    }

    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, CommandHandlerResponse<Guid>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateClientCommandHandler(
            IApplicationDbContext context,
            IMapper mapper
            )
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<CommandHandlerResponse<Guid>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Clients
                .FirstOrDefaultAsync(client => client.Email == request.Email);

            if (user != null)
                throw new ConflictException($"Entity {nameof(user)} with email {request.Email} already exists in database");


            var newUser = _mapper.Map<Client>(request);

            await _dbContext.Clients.AddAsync(newUser);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CommandHandlerResponse<Guid>() { Response = newUser.Id } ;
        }
    }
}