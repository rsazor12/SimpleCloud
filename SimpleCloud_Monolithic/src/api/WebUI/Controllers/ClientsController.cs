using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleCloud_Monolithic.Application.Clients.Commands.CreateClient;
using SimpleCloudMonolithic.WebUI.Controllers;

namespace SimpleCloud_Monolithic.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateClient(CreateClientCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}