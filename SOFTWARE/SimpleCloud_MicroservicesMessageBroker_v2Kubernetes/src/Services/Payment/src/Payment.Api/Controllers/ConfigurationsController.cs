using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Payment_SimpleCloud_MicroservicesHttp.Application.Common.Configurations;
using Payment_SimpleCloud_MicroservicesHttp.Application.Common.Interfaces;
using Payment_SimpleCloud_MicroservicesHttp.Controllers;

namespace Payment_SimpleCloud_MicroservicesHttp.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationsController : ApiController
    {
        private readonly IPaymentDbContext _paymentDbContext;

        private readonly AppSettings _appSettings;


        public ConfigurationsController(
            IOptions<AppSettings> settings,
            IPaymentDbContext paymentDbContext)
        {
            _appSettings = settings.Value;
            _paymentDbContext = paymentDbContext;
        }

        //public ConfigurationsController(IIdentityDbContext dbContext, IMapper mapper)
        //{
        //    _identityDbContext = dbContext;
        //    _mapper = mapper;
        //}

        [HttpPost("clearDatabase")]
        public async Task<ActionResult> ClearDatabase()
        {
            //var clients = _identityDbContext.Clients.ToList();
            //_identityDbContext.Clients.RemoveRange(clients);

            _paymentDbContext.Clients.RemoveRange(_paymentDbContext.Clients);
            _paymentDbContext.ClientTasks.RemoveRange(_paymentDbContext.ClientTasks);

            await _paymentDbContext.SaveChangesAsync(default);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetConfiguration()
        {
            return _appSettings;
        }
    }
}