﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Payment_SimpleCloud_MicroservicesHttp.Application.Clients.Commands.CreateClient;
using Payment_SimpleCloud_MicroservicesHttp.Application.Models.HandlerResponse;
using Payment_SimpleCloud_MicroservicesHttp.Controllers;

namespace Payment_SimpleCloud_MicroservicesHttp.WebUI.Controllers
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
