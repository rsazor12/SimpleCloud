using AutoMapper;
using Identity_SimpleCloud_MicroservicesHttp.Domain.Entities;
using SimpleCloudMonolithic.Application.Common.Mappings;

namespace Identity_SimpleCloud_MicroservicesHttp.Application.Clients.Commands.CreateClient
{
    public class CreateClientCommand: IMapFrom<Client>
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

    //public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, CommandHandlerResponse<Guid>>
    //{
    //    private readonly IApplicationDbContext _dbContext;
    //    private readonly IMapper _mapper;

    //    public CreateClientCommandHandler(
    //        IApplicationDbContext context,
    //        IMapper mapper
    //        )
    //    {
    //        _dbContext = context;
    //        _mapper = mapper;
    //    }

    //    public async Task<CommandHandlerResponse<Guid>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    //    {
    //        var user = await _dbContext.Clients
    //            .FirstOrDefaultAsync(client => client.Email == request.Email);

    //        if (user != null)
    //            throw new ConflictException($"Entity {nameof(user)} with email {request.Email} already exists in database");


    //        var newUser = _mapper.Map<Client>(request);

    //        await _dbContext.Clients.AddAsync(newUser);

    //        await _dbContext.SaveChangesAsync(cancellationToken);

    //        return new CommandHandlerResponse<Guid>() { Response = newUser.Id } ;
    //    }
    // }
}