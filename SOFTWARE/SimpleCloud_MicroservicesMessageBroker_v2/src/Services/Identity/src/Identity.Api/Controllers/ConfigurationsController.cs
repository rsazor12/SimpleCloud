using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Identity_SimpleCloud_MicroservicesHttp.Application.Clients.Commands.CreateClient;
using Identity_SimpleCloud_MicroservicesHttp.Application.Models.HandlerResponse;
using Identity_SimpleCloud_MicroservicesHttp.Application.Common.Interfaces;
using Identity_SimpleCloud_MicroservicesHttp.WebUI.Controllers;

namespace Identity_SimpleCloud_MicroservicesHttp.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationsController : ApiController
    {
        private readonly IIdentityDbContext _identityDbContext;
        private readonly IMapper _mapper;

        public ConfigurationsController(IIdentityDbContext dbContext, IMapper mapper)
        {
            _identityDbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPost("configurations/clearDatabase")]
        public async Task<ActionResult<CommandHandlerResponse<Guid>>> ClearDatabase(ClearDatabaseCommand command)
        {
            var clients = _identityDbContext.Clients.ToList();
            _identityDbContext.Clients.RemoveRange(clients);

            return null;
        }
    }
}