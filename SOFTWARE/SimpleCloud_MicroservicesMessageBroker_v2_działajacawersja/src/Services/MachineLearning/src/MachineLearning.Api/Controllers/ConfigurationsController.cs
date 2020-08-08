using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MachineLearning_SimpleCloud_MicroservicesHttp.Controllers;
using MachineLearning_SimpleCloud_MicroservicesHttp.Application.Configurations.Commands;

namespace MachineLearning_SimpleCloud_MicroservicesHttp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationsController : ApiController
    {
        [HttpPost("clearDatabase")]
        public async Task<ActionResult> ClearDatabase(ClearDatabaseCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}