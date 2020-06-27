using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SimpleCloud_Monolithic.Application.Common.Configurations;
using SimpleCloud_Monolithic.Application.Common.CQRS;
using SimpleCloud_Monolithic.Application.Common.Mappings;
using SimpleCloud_Monolithic.Application.MLServices.Commands.MakePrediction;
using SimpleCloud_Monolithic.Application.Models;
using SimpleCloud_Monolithic.Application.Models.DTO;
using SimpleCloud_Monolithic.Application.Models.HandlerResponse;
using SimpleCloud_Monolithic.Domain.Entities;
using SimpleCloudMonolithic.Application.Common.Exceptions;
using SimpleCloudMonolithic.Application.Common.Interfaces;
using SimpleCloudMonolithic.Application.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleCloud_Monolithic.Application.MLServices.Commands
{
    public class MakePredictionCommand : IBillableRequest, IRequest<CommandHandlerResponse<IEnumerable<MakePredictionCommandVM>>>
    {
        public Guid MLServiceId { get; set; }
    }

    public class MakepredictionCommandHandler : IRequestHandler<MakePredictionCommand, CommandHandlerResponse<IEnumerable<MakePredictionCommandVM>>>
    {
        private readonly IApplicationDbContext _dbContext;
        private IModelBuilder _modelBuilder;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public MakepredictionCommandHandler(
            IModelBuilder modelBuilder,
            IApplicationDbContext dbContext,
            IOptions<AppSettings> settings,
            IMapper mapper
        )
        {
            _modelBuilder = modelBuilder;
            _dbContext = dbContext;
            _appSettings = settings.Value;
            _mapper = mapper;
        }

        public async Task<CommandHandlerResponse<IEnumerable<MakePredictionCommandVM>>> Handle(MakePredictionCommand request, CancellationToken cancellationToken)
        {
            var mlService = await _dbContext.MLServices
               .Include(mlService => mlService.ServiceDetails)
               .Include(mlService => mlService.Client)
               //.ProjectTo<MLService>(_mapper.ConfigurationProvider)
               .FirstOrDefaultAsync(mlService => mlService.Id == request.MLServiceId)
               ?? throw new NotFoundException(nameof(MLService), request.MLServiceId);

            // TODO - extract to another method
            var learningFiles = Directory.GetFiles(mlService.ServiceDetails.TestDataPath)
                .Select(filePath =>
                {
                    var filePathSegments = filePath.Split(".");
                    var label = filePathSegments[filePathSegments.Count() - 2];

                    return new ModelInput() { Label = label, ImageSource = filePath };
                });

            var modelPath = mlService.ServiceDetails.ModelPath;

            var predictionResult =
                _modelBuilder
                .Predict(modelPath, learningFiles)
                .Select((prediction, index) => new MakePredictionCommandVM()
                {
                    FileName = learningFiles.ElementAt(index).ImageSource,
                    ModelOutputDTO = _mapper.Map<ModelOutputDTO>(prediction)
                });

            //Console.WriteLine("Using model to make single prediction -- Comparing actual Label with predicted Label from sample data...\n\n");
            //Console.WriteLine($"ImageSource: {testFile.ImageSource}");
            //Console.WriteLine($"\n\nActual Label: {testFile.Label} \nPredicted Label value {predictionResult.Prediction} \nPredicted Label scores: [{String.Join(",", predictionResult.Score)}]\n\n");

            return new CommandHandlerResponse<IEnumerable<MakePredictionCommandVM>>() { Response = predictionResult };
        }
    }
}
