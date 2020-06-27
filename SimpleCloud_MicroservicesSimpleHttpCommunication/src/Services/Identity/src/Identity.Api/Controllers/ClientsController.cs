using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Identity_SimpleCloud_MicroservicesHttp.Application.Clients.Commands.CreateClient;
using Identity_SimpleCloud_MicroservicesHttp.Application.Common.Exceptions;
using Identity_SimpleCloud_MicroservicesHttp.Application.Models.HandlerResponse;
using Identity_SimpleCloud_MicroservicesHttp.Domain.Entities;
using SimpleCloudMonolithic.Application.Common.Interfaces;
using SimpleCloudMonolithic.WebUI.Controllers;

namespace Identity_SimpleCloud_MicroservicesHttp.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ApiController
    {
        private readonly IIdentityDbContext _identityDbContext;
        private readonly IMapper _mapper;

        public ClientsController(IIdentityDbContext dbContext, IMapper mapper)
        {
            _identityDbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<CommandHandlerResponse<Guid>>> CreateClient(CreateClientCommand command)
        {
            var user = await _identityDbContext.Clients
                .FirstOrDefaultAsync(client => client.Email == command.Email);

            if (user != null)
                throw new ConflictException($"Entity {nameof(user)} with email {command.Email} already exists in database");


            var newUser = _mapper.Map<Client>(command);

            await _identityDbContext.Clients.AddAsync(newUser);

            await _identityDbContext.SaveChangesAsync(default(CancellationToken));

            return new CommandHandlerResponse<Guid>() { Response = newUser.Id };
        }

        //[HttpPost]
        //public async Task<ActionResult<CommandHandlerResponse<Guid>>> CreateClient(CreateClientCommand command)
        //{
        //    return Ok(await Mediator.Send(command));

        //}

        //[HttpGet]
        //public async Task<ActionResult<CommandHandlerResponse<Guid>>> CreateClientz(CreateClientCommand command)
        //{
        //    return Ok(await Mediator.Send(command));

        //}
    }
}