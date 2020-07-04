using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MachineLearning_SimpleCloud_MicroservicesHttp.Controllers;
using MachineLearning_SimpleCloud_MicroservicesHttp.Application.Models.HandlerResponse;
using MachineLearning_SimpleCloud_MicroservicesHttp.Application.Clients.Commands.CreateClient;

namespace MachineLearning_SimpleCloud_MicroservicesHttp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<CommandHandlerResponse<Guid>>> CreateClient(CreateClientCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
