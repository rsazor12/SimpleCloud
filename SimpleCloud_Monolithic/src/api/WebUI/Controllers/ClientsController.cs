using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleCloud_Monolithic.Application.Clients.Commands.CreateClient;
using SimpleCloud_Monolithic.Application.Models.HandlerResponse;
using SimpleCloudMonolithic.WebUI.Controllers;

namespace SimpleCloud_Monolithic.WebUI.Controllers
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

        //[HttpGet]
        //public async Task<ActionResult<CommandHandlerResponse<Guid>>> CreateClientz(CreateClientCommand command)
        //{
        //    return Ok(await Mediator.Send(command));

        //}
    }
}