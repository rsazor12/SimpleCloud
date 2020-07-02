using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleCloud_Monolithic.Application.Configurations.Commands;
using SimpleCloudMonolithic.WebUI.Controllers;

namespace SimpleCloud_Monolithic.WebUI.Controllers
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