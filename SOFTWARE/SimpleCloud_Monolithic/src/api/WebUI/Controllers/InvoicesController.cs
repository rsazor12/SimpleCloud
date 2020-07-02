using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleCloud_Monolithic.Application.Invoice.Queries.GetInvoice;
using SimpleCloudMonolithic.Application.WeatherForecasts.Queries.GetWeatherForecasts;
using SimpleCloudMonolithic.WebUI.Controllers;

namespace SimpleCloud_Monolithic.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ApiController
    {
        [HttpGet("{mlServiceId}")]
        public async Task<IActionResult> Get(Guid mlServiceId)
        {
            // return Ok();
            var result = await Mediator.Send(new GetInvoiceQuery() { MLServiceId = mlServiceId });

            return Ok(result);
        }
    }
}
