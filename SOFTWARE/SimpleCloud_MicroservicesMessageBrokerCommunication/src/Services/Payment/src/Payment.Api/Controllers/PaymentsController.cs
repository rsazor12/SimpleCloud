using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Payment_SimpleCloud_MicroservicesMessageBroker.Application.Invoice.Queries.GetInvoice;
using Payment_SimpleCloud_MicroservicesMessageBroker.Controllers;

namespace Payment_SimpleCloud_MicroservicesMessageBroker.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ApiController
    {
        [HttpPost("{mlServiceId}")]
        public async Task<IActionResult> Get(Guid mlServiceId)
        {
            // return Ok();
            var result = await Mediator.Send(new GetInvoiceQuery() { MLServiceId = mlServiceId });

            return Ok(result);
        }
    }
}
