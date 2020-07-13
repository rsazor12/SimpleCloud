using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Payment_SimpleCloud_MicroservicesHttp.Application.ClientTasks.Commands;
using Payment_SimpleCloud_MicroservicesHttp.Application.Models.HandlerResponse;
using Payment_SimpleCloud_MicroservicesHttp.Controllers;

namespace Payment_SimpleCloud_MicroservicesHttp.WebUI.Controllers
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
