using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MachineLearning_SimpleCloud_Broker.Controllers;
using MachineLearning_SimpleCloud_Broker.Application.Models.HandlerResponse;
using MachineLearning_SimpleCloud_Broker.Application.Clients.Commands.CreateClient;

namespace MachineLearning_SimpleCloud_Broker.Controllers
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
