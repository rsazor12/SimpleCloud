using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Payment_SimpleCloud_MicroservicesMessageBroker.Application.ClientTasks.Commands;
using Payment_SimpleCloud_MicroservicesMessageBroker.Application.Models.HandlerResponse;
using Payment_SimpleCloud_MicroservicesMessageBroker.Controllers;

namespace Payment_SimpleCloud_MicroservicesMessageBroker.WebUI.Controllers
{
    public class ClientTasksController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<CommandHandlerResponse<Guid>>> CreateClient(AddClientTaskCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
