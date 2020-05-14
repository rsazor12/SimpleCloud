using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using SimpleCloud_Monolithic.Application.MLServices.Commands;
using SimpleCloud_Monolithic.Application.MLServices.Commands.CreateLearningService;
using SimpleCloud_Monolithic.Application.MLServices.Commands.UpdateMLService;
using SimpleCloud_Monolithic.Application.MLServices.Commands.UploadLearningFiles;
using SimpleCloud_Monolithic.Application.MLServices.Commands.UploadPredictionFiles;

namespace SimpleCloudMonolithic.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MLServicesController : ApiController
    {

        [HttpPost]
        public async Task<ActionResult<int>> OrderMLService(OrderMLServiceCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<ActionResult<int>> UpdateMLService(UpdateMLServiceCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("{mlServiceId}/learning")]
        public async Task<ActionResult<int>> PerformLearning(Guid mlServiceId)
        {
            var command = new PerformLearningCommand() { MLServiceId = mlServiceId};
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("{mlServiceId}/learning/files")]
        public async Task<ActionResult> UploadLearningFiles(Guid mlServiceId, [FromForm]IFormFileCollection files)
        {
            var command = new UploadLearningFilesCommand() { MLServiceId = mlServiceId, files = files };
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("{mlServiceId}/prediction")]
        public async Task<ActionResult> MakePrediction(Guid mlServiceId)
        {
            var command = new MakePredictionCommand() { MLServiceId = mlServiceId};
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("{mlServiceId}/prediction/files")]
        public async Task<ActionResult> UploadPredictionFiles(Guid mlServiceId, [FromForm]IFormFileCollection files)
        {
            var command = new UploadPredictionFilesCommand() { MLServiceId = mlServiceId, files = files };
            return Ok(await Mediator.Send(command));
        }
    }
}