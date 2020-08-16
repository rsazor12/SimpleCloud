using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MachineLearning_SimpleCloud_MicroservicesHttp.Controllers;
using MachineLearning_SimpleCloud_MicroservicesHttp.Application.Configurations.Commands;
using MachineLearning_SimpleCloud_MicroservicesHttp.Application.Common.Configurations;
using Microsoft.Extensions.Options;

namespace MachineLearning_SimpleCloud_MicroservicesHttp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationsController : ApiController
    {
        private readonly AppSettings _appSettings;

        public ConfigurationsController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        [HttpPost("clearDatabase")]
        public async Task<ActionResult> ClearDatabase()
        {
            var command = new ClearDatabaseCommand();

            return Ok(await Mediator.Send(command));
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetConfiguration()
        {
            return _appSettings;
        }
    }
}