//using AutoMapper;
//using Identity_SimpleCloud_MicroservicesMessageBroker.Domain.Entities;
//using Identity_SimpleCloud_MicroservicesMessageBroker.Application.Common.Mappings;
//using Identity_SimpleCloud_MicroservicesMessageBroker.Application.Models.HandlerResponse;
//using System;
//using Identity_SimpleCloud_MicroservicesMessageBroker.Application.Common.Interfaces;
//using System.Threading;
//using System.Threading.Tasks;
//using Identity_SimpleCloud_MicroservicesMessageBroker.Application.Common.Exceptions;
//using Microsoft.EntityFrameworkCore;
//using MediatR;

namespace Identity_SimpleCloud_MicroservicesMessageBroker.Application.Clients.Commands.CreateClient
{
    public class CreateClientCommand // : IMapFrom<Client>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        //public void Mapping(Profile profile)
        //{
        //    profile.CreateMap<CreateClientCommand, Client>()
        //        .ForMember(user => user.Id, opt => opt.Ignore());
        //}
    }
}

//    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, CommandHandlerResponse<Guid>>
//    {
//        private readonly IIdentityDbContext _identityDbContext;
//        private readonly IMapper _mapper;

//        public CreateClientCommandHandler(
//            IIdentityDbContext context,
//            IMapper mapper
//            )
//        {
//            _identityDbContext = context;
//            _mapper = mapper;
//        }

//        public async Task<CommandHandlerResponse<Guid>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
//        {
//            var user = await _identityDbContext.Clients
//                .FirstOrDefaultAsync(client => client.Email == request.Email);

//            if (user != null)
//                throw new ConflictException($"Entity {nameof(user)} with email {request.Email} already exists in database");


//            var newUser = _mapper.Map<Client>(request);

//            await _identityDbContext.Clients.AddAsync(newUser);

//            await _identityDbContext.SaveChangesAsync(cancellationToken);

//            return new CommandHandlerResponse<Guid>() { Response = newUser.Id };
//        }
//    }
//}