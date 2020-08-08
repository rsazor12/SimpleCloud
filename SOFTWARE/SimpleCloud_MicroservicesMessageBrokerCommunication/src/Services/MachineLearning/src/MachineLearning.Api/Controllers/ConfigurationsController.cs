using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MachineLearning_SimpleCloud_Broker.Controllers;
using MachineLearning_SimpleCloud_Broker.Application.Configurations.Commands;

namespace MachineLearning_SimpleCloud_Broker.Controllers
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