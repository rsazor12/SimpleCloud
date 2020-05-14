using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SimpleCloud_Monolithic.Application.Common.Configurations;
using SimpleCloud_Monolithic.Application.Models;
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
    public class MakePredictionCommand : IRequest<IEnumerable<ModelOutput>>
    {
        public Guid MLServiceId { get; set; }
    }

    public class MakepredictionCommandHandler : IRequestHandler<MakePredictionCommand, IEnumerable<ModelOutput>>
    {
        private readonly IApplicationDbContext _dbContext;
        private IModelBuilder _modelBuilder;
        private readonly AppSettings _appSettings;

        public MakepredictionCommandHandler(
            IModelBuilder modelBuilder,
            IApplicationDbContext dbContext,
            IOptions<AppSettings> settings
        )
        {
            _modelBuilder = modelBuilder;
            _dbContext = dbContext;
            _appSettings = settings.Value;
        }

        public async Task<IEnumerable<ModelOutput>> Handle(MakePredictionCommand request, CancellationToken cancellationToken)
        {

            // Create single instance of sample data from first line of dataset for model input
            // ModelInput sampleData = CreateSingleDataSample(DATA_FILEPATH);

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

            // var testFile = learningFiles.First();
            var modelPath = mlService.ServiceDetails.ModelPath;


            // Make a single prediction on the sample data and print results
            var predictionResult = _modelBuilder.Predict(modelPath, learningFiles);


            //Console.WriteLine("Using model to make single prediction -- Comparing actual Label with predicted Label from sample data...\n\n");
            //Console.WriteLine($"ImageSource: {testFile.ImageSource}");
            //Console.WriteLine($"\n\nActual Label: {testFile.Label} \nPredicted Label value {predictionResult.Prediction} \nPredicted Label scores: [{String.Join(",", predictionResult.Score)}]\n\n");

            return predictionResult;
        }

        //private static ModelInput CreateSingleDataSample(string dataFilePath)
        //{
        //    // Create MLContext
        //    MLContext mlContext = new MLContext();

        //    // Load dataset
        //    IDataView dataView = mlContext.Data.LoadFromTextFile<ModelInput>(
        //                                    path: dataFilePath,
        //                                    hasHeader: true,
        //                                    separatorChar: '\t',
        //                                    allowQuoting: true,
        //                                    allowSparse: false);

        //    // Use first line of dataset as model input
        //    // You can replace this with new test data (hardcoded or from end-user application)
        //    ModelInput sampleForPrediction = mlContext.Data.CreateEnumerable<ModelInput>(dataView, false)
        //                                                                .First();
        //    return sampleForPrediction;
        //}
    }
}
