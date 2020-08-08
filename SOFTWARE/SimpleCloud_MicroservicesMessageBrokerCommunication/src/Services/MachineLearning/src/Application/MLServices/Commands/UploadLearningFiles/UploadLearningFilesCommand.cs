using MachineLearning_SimpleCloud_Broker.Domain.Entities;
using MediatR;
using MachineLearning_SimpleCloud_Broker.Application.Common.Exceptions;
using MachineLearning_SimpleCloud_Broker.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using MachineLearning_SimpleCloud_Broker.Application.Common.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MachineLearning_SimpleCloud_Broker.Application.Models.HandlerResponse;
using MachineLearning_SimpleCloud_Broker.Application.Common.CQRS;

namespace MachineLearning_SimpleCloud_Broker.Application.MLServices.Commands.UploadLearningFiles
{
    public class UploadLearningFilesCommand : IBillableRequest, IRequest<CommandHandlerResponse>
    {
        public Guid MLServiceId { get; set; }

        public IEnumerable<IFormFile> files { get; set; }
    }

    public class UploadLearningFilesCommandHandler : IRequestHandler<UploadLearningFilesCommand, CommandHandlerResponse>
    {
        private readonly IMachineLearningDbContext _dbContext;
        private readonly AppSettings AppSettings;
        private readonly IMapper _mapper;

        public UploadLearningFilesCommandHandler(
            IOptions<AppSettings> settings,
            IMachineLearningDbContext dbContext,
            IMapper mapper)
        {
            AppSettings = settings.Value;
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task<CommandHandlerResponse> Handle(UploadLearningFilesCommand request, CancellationToken cancellationToken)
        {
            var mlService = await _dbContext.MLServices
                .Include(mlService => mlService.ServiceDetails)
                .Include(mlService => mlService.Client)
                //.ProjectTo<MLService>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(mlService => mlService.Id == request.MLServiceId)
                ?? throw new NotFoundException(nameof(MLService), request.MLServiceId);

            try
            {
                string path = Path.Combine(AppSettings.FileStorageSettings.Path, request.MLServiceId + "/Images/Learning/");

                Directory.CreateDirectory(path);

                foreach (IFormFile learningFile in request.files)
                {
                    var filePath = path + learningFile.FileName;

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await learningFile.CopyToAsync(stream);
                    }
                }

                mlService.UpdateLearningFilesPath(path);

                await _dbContext.SaveChangesAsync(cancellationToken);
            } 
            catch (Exception ex)
            {
                // TODO - revert uploaded files

                throw ex;
            }
            

            return new CommandHandlerResponse();
        }
    }
}
