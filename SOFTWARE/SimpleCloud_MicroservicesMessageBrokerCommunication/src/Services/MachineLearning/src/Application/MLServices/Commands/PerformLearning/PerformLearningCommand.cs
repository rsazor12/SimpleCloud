using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using MachineLearning_SimpleCloud_Broker.Application.Common.Configurations;
using MachineLearning_SimpleCloud_Broker.Application.Common.CQRS;
using MachineLearning_SimpleCloud_Broker.Application.Models.HandlerResponse;
using MachineLearning_SimpleCloud_Broker.Domain.Entities;
using MachineLearning_SimpleCloud_Broker.Application.Common.Exceptions;
using MachineLearning_SimpleCloud_Broker.Application.Common.Interfaces;
using MachineLearning_SimpleCloud_Broker.Application.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MachineLearning_SimpleCloud_Broker.Application.MLServices.Commands
{
    public class PerformLearningCommand : IBillableRequest, IRequest<CommandHandlerResponse>
    {
        // [FromQuery(Name = "mlServiceId")]
        // [BindProperty]
        public Guid MLServiceId { get; set; }
        // public string TrainDataFilePath { get; set; }
    }

    public class PerformLearningCommandHandler : IRequestHandler<PerformLearningCommand, CommandHandlerResponse>
    {
        private readonly IMachineLearningDbContext _dbContext;
        private IModelBuilder _modelBuilder;
        private readonly AppSettings _appSettings;

        public PerformLearningCommandHandler(
            IModelBuilder modelBuilder,
            IMachineLearningDbContext dbContext,
            IOptions<AppSettings> settings
        )
        {
            _modelBuilder = modelBuilder;
            _dbContext = dbContext;
            _appSettings = settings.Value;
        }

        async Task<CommandHandlerResponse> IRequestHandler<PerformLearningCommand, CommandHandlerResponse>.Handle(PerformLearningCommand request, CancellationToken cancellationToken)
        {

            var mlService = await _dbContext.MLServices
                .Include(mlService => mlService.ServiceDetails)
                .Include(mlService => mlService.Client)
                //.ProjectTo<MLService>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(mlService => mlService.Id == request.MLServiceId)
                ?? throw new NotFoundException(nameof(MLService), request.MLServiceId);

            var learningFiles = Directory.GetFiles(mlService.ServiceDetails.TrainDataPath)
                .Select(filePath =>
                {
                    var filePathSegments = filePath.Split(".");
                    var label = filePathSegments[filePathSegments.Count() - 2];

                    return new ModelInput() { Label = label, ImageSource = filePath };
                });


            var files = Directory.GetFiles(mlService.ServiceDetails.TrainDataPath);

            var (mlContext, mlModel, dataViewSchema) = _modelBuilder.CreateModel(learningFiles);

            var modelPath = _appSettings.FileStorageSettings.Path + "/" + request.MLServiceId + "/Models";

            Directory.CreateDirectory(modelPath);

            modelPath += "/model.zip";

            _modelBuilder.SaveModel(mlContext, mlModel, modelPath, dataViewSchema);

            mlService.UpdateModelPath(modelPath);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CommandHandlerResponse();
        }
    }
}
