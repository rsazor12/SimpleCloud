using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleCloudMonolithic.Application.MachineLearning.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using SimpleCloud_Monolithic.Application.MLServiceCommands.UploadLearningFiles;
using System.IO;
using SimpleCloud_Monolithic.Application.MLServiceCommands.CreateLearningService;
using SimpleCloud_Monolithic.Application.MLServiceCommands.UpdateMLService;

namespace SimpleCloudMonolithic.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MLServicesController : ApiController
    {
        private IWebHostEnvironment hostingEnvironment;

        public MLServicesController(IWebHostEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

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

        [HttpPost("learning")]
        public async Task<ActionResult<int>> PerformLearning()
        {
            var command = new PerformLearningCommand();
            return Ok(await Mediator.Send(command));
        }


        [HttpPost("learining/files")]
        public async Task<ActionResult> UploadLearningFiles([FromForm] IEnumerable<IFormFile> learningFiles)
        {
            foreach(IFormFile learningFile in learningFiles)
            {
                string fName = learningFile.FileName;
                string path = Path.Combine(hostingEnvironment.ContentRootPath, "Images/" + learningFile.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await learningFile.CopyToAsync(stream);
                }
            }

            return Ok();
        }
    }
}