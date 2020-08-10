using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Identity_SimpleCloud_MicroservicesHttp.Application.Clients.Commands.CreateClient;
using Identity_SimpleCloud_MicroservicesHttp.Application.Models.HandlerResponse;
using Identity_SimpleCloud_MicroservicesHttp.Application.Common.Interfaces;
using Identity_SimpleCloud_MicroservicesHttp.WebUI.Controllers;
using Identity_SimpleCloud_MicroservicesHttp.Application.Common.Configurations;
using Microsoft.Extensions.Options;

namespace Identity_SimpleCloud_MicroservicesHttp.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationsController : ApiController
    {
        private readonly IIdentityDbContext _identityDbContext;
        private readonly IMapper _mapper;

        private readonly AppSettings _appSettings;


        public ConfigurationsController(
            IOptions<AppSettings> settings,
            IMapper mapper,
            IIdentityDbContext dbContext)
        {
            _appSettings = settings.Value;
            _identityDbContext = dbContext;
            _mapper = mapper;
        }

        //public ConfigurationsController(IIdentityDbContext dbContext, IMapper mapper)
        //{
        //    _identityDbContext = dbContext;
        //    _mapper = mapper;
        //}

        [HttpPost("configurations/clearDatabase")]
        public async Task<ActionResult<CommandHandlerResponse<Guid>>> ClearDatabase(ClearDatabaseCommand command)
        {
            var clients = _identityDbContext.Clients.ToList();
            _identityDbContext.Clients.RemoveRange(clients);

            return null;
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetConfiguration()
        {
            return _appSettings.ConnectionStrings.DefaultConnection;
        }



    }
}