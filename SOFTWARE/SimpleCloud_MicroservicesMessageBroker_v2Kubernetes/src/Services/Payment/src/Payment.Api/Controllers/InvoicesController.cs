using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Payment_SimpleCloud_MicroservicesHttp.Application.Invoice.Queries.GetInvoice;
using Payment_SimpleCloud_MicroservicesHttp.Controllers;

namespace Payment_SimpleCloud_MicroservicesHttp.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ApiController
    {
        [HttpGet("{clientId}")]
        public async Task<IActionResult> Get(Guid clientId)
        {
            // return Ok();
            var result = await Mediator.Send(new GetInvoiceQuery() { ClientId = clientId });

            return Ok(result);
        }
    }
}
