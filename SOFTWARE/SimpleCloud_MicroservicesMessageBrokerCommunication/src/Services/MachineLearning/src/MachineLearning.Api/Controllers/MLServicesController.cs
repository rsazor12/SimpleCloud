using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using MachineLearning_SimpleCloud_Broker.Application.Models.HandlerResponse;
using MachineLearning_SimpleCloud_Broker.Application.MLServices.Commands.CreateLearningService;
using MachineLearning_SimpleCloud_Broker.Application.MLServices.Commands;
using MachineLearning_SimpleCloud_Broker.Application.MLServices.Commands.UploadLearningFiles;
using MachineLearning_SimpleCloud_Broker.Application.MLServices.Commands.MakePrediction;
using MachineLearning_SimpleCloud_Broker.Application.MLServices.Commands.UploadPredictionFiles;
using MachineLearning_SimpleCloud_Broker.Application.MLServices.Commands.UpdateMLService;

namespace MachineLearning_SimpleCloud_Broker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MLServicesController : ApiController
    {

        [HttpPost]
        public async Task<ActionResult<CommandHandlerResponse<Guid>>> CreateMLService(CreateMLServiceCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<ActionResult<CommandHandlerResponse>> UpdateMLService(UpdateMLServiceCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("{mlServiceId}/learning")]
        public async Task<ActionResult<CommandHandlerResponse>> PerformLearning(Guid mlServiceId)
        {
            var command = new PerformLearningCommand() { MLServiceId = mlServiceId};
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("{mlServiceId}/learning/files")]
        public async Task<ActionResult<CommandHandlerResponse>> UploadLearningFiles(Guid mlServiceId, [FromForm]IFormFileCollection files)
        {
            var command = new UploadLearningFilesCommand() { MLServiceId = mlServiceId, files = files };
            var res = await Mediator.Send(command);

            return Ok(res);
        }

        [HttpPost("{mlServiceId}/prediction")]
        public async Task<ActionResult<CommandHandlerResponse<IEnumerable<MakePredictionCommandVM>>>> MakePrediction(Guid mlServiceId)
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