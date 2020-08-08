using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Vision;
using MachineLearning_SimpleCloud_MicroservicesHttp.Application.Models;
using MachineLearning_SimpleCloud_MicroservicesHttp.Application.Common.Interfaces;
using MachineLearning_SimpleCloud_MicroservicesHttp.Application.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MachineLearning_SimpleCloud_MicroservicesHttp.Infrastructure.Services
{
    public class ModelBuilder : IModelBuilder
    {
        public MLContext mlContext { get; set; } = new MLContext(seed: 1);
        //private string TRAIN_DATA_FILEPATH = @"C:\Users\Y520\AppData\Local\Temp\50509f5c-5103-4276-a606-83edeb9b6dd0.tsv";
        //private string MODEL_FILEPATH = @"C:\Users\Y520\AppData\Local\Temp\MLVSTools\ML_DOTNET_IMAGE_CLASSIFICATIONML\ML_DOTNET_IMAGE_CLASSIFICATIONML.Model\MLModel.zip";
        // Create MLContext to be shared across the model creation workflow objects 
        // Set a random seed for repeatable/deterministic results across multiple trainings.

        private readonly ILogger<ModelBuilder> _logger;

        public ModelBuilder(
            ILogger<ModelBuilder> logger
            )
        {
            _logger = logger;
        }


        (MLContext, ITransformer, DataViewSchema) IModelBuilder.CreateModel(IEnumerable<ModelInput> modelInput)
        {
            // Load Data
            IDataView trainingDataView = mlContext.Data.LoadFromEnumerable(modelInput);

            // Build training pipeline
            IEstimator<ITransformer> trainingPipeline = BuildTrainingPipeline(mlContext);

            // Evaluate quality of Model
            // Evaluate(mlContext, trainingDataView, trainingPipeline);

            // Train Model
            ITransformer mlModel = TrainModel(mlContext, trainingDataView, trainingPipeline);


            return (mlContext, mlModel, trainingDataView.Schema);
        }

        public void SaveModel(
            MLContext mlContext,
            ITransformer mlModel,
            string modelRelativePath,
            DataViewSchema modelInputSchema
        )
        {
            // Save/persist the trained model to a .ZIP file
            Console.WriteLine($"=============== Saving the model  ===============");
            mlContext.Model.Save(mlModel, modelInputSchema, GetAbsolutePath(modelRelativePath));
            Console.WriteLine("The model is saved to {0}", GetAbsolutePath(modelRelativePath));
        }

        public IEnumerable<ModelOutput> Predict(string modelPath, IEnumerable<ModelInput> predictionItems)
        {
            //Create MLContext
            MLContext mlContext = new MLContext();

            // Load Trained Model
            DataViewSchema predictionPipelineSchema;
            ITransformer predictionPipeline = mlContext.Model.Load(modelPath, out predictionPipelineSchema);
            
            // Create PredictionEngines
            //var predictionEngine
            //    = mlContext.Model.CreatePredictionEngine<List<ModelInput>, List<ModelOutput>>(predictionPipeline);

            IDataView items = mlContext.Data.LoadFromEnumerable(predictionItems);

            IDataView predictions = predictionPipeline.Transform(items);

            // var scoreColumn = predictions.GetColumn<float[]>("Score");


            // Create an IEnumerable of HousingData objects from IDataView
            IEnumerable<ModelOutput> output =
                mlContext.Data.CreateEnumerable<ModelOutput>(predictions, reuseRowObject: true);

            // Iterate over each row
            //foreach (var row in output)
            //{
            //    // Do something (print out Size property) with current Housing Data object being evaluated
            //    Console.WriteLine(row.Prediction);
            //}

            return output;
        }

        private IEstimator<ITransformer> BuildTrainingPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations 
            var dataProcessPipeline = mlContext.Transforms.Conversion.MapValueToKey("Label", "Label")
                                      .Append(mlContext.Transforms.LoadRawImageBytes("ImageSource_featurized", null, "ImageSource"))
                                      .Append(mlContext.Transforms.CopyColumns("Features", "ImageSource_featurized"));
            // Set the training algorithm 
            var trainer = mlContext.MulticlassClassification.Trainers.ImageClassification(new ImageClassificationTrainer.Options() { LabelColumnName = "Label", FeatureColumnName = "Features", Arch = ImageClassificationTrainer.Architecture.ResnetV250 })
                                      .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel", "PredictedLabel"));

            //.Append(mlContext.Model.LoadTensorFlowModel(inputTensorFlowModelFilePath));

            // var trainer = mlContext.Model.LoadTensorFlowModel("./PretrainedModels/inception_v3.meta")               
                                 //.ScoreTensorFlowModel(outputColumnNames: new[] { "InceptionV3/Predictions/Reshape" },
                                 //                     inputColumnNames: new[] { "Label" },
                                 //                     addBatchDimensionInput: false);

            // var trainer2 = mlContext.MulticlassClassification.Trainers


            var trainingPipeline = dataProcessPipeline.Append(trainer);

            return trainingPipeline;
        }

        private ITransformer TrainModel(MLContext mlContext, IDataView trainingDataView, IEstimator<ITransformer> trainingPipeline)
        {
            _logger.LogInformation("=============== Training  model ===============");
            mlContext.Log += LogMLEvent;
            ITransformer model = trainingPipeline.Fit(trainingDataView);
            
            _logger.LogInformation("=============== End of training process ===============");
            return model;
        }

        private void Evaluate(MLContext mlContext, IDataView trainingDataView, IEstimator<ITransformer> trainingPipeline)
        {
            // Cross-Validate with single dataset (since we don't have two datasets, one for training and for evaluate)
            // in order to evaluate and get the model's accuracy metrics
            // Console.WriteLine("=============== Cross-validating to get model's accuracy metrics ===============");
            _logger.LogInformation("=============== Cross-validating to get model's accuracy metrics ===============");
            mlContext.Log += LogMLEvent;
            var crossValidationResults = mlContext.MulticlassClassification.CrossValidate(trainingDataView, trainingPipeline, numberOfFolds: 5, labelColumnName: "Label");
            PrintMulticlassClassificationFoldsAverageMetrics(crossValidationResults);
        }

        private void LogMLEvent(object sender, Microsoft.ML.LoggingEventArgs e)
        {
            _logger.LogInformation(e.Message);
        }

        private string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(ModelBuilder).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }

        private void PrintMulticlassClassificationMetrics(MulticlassClassificationMetrics metrics)
        {
            _logger.LogInformation($"************************************************************");
            _logger.LogInformation($"*    Metrics for multi-class classification model   ");
            _logger.LogInformation($"*-----------------------------------------------------------");
            _logger.LogInformation($"    MacroAccuracy = {metrics.MacroAccuracy:0.####}, a value between 0 and 1, the closer to 1, the better");
            _logger.LogInformation($"    MicroAccuracy = {metrics.MicroAccuracy:0.####}, a value between 0 and 1, the closer to 1, the better");
            _logger.LogInformation($"    LogLoss = {metrics.LogLoss:0.####}, the closer to 0, the better");
            for (int i = 0; i < metrics.PerClassLogLoss.Count; i++)
            {
                _logger.LogInformation($"    LogLoss for class {i + 1} = {metrics.PerClassLogLoss[i]:0.####}, the closer to 0, the better");
            }
            _logger.LogInformation($"************************************************************");
        }

        private void PrintMulticlassClassificationFoldsAverageMetrics(IEnumerable<TrainCatalogBase.CrossValidationResult<MulticlassClassificationMetrics>> crossValResults)
        {
            var metricsInMultipleFolds = crossValResults.Select(r => r.Metrics);

            var microAccuracyValues = metricsInMultipleFolds.Select(m => m.MicroAccuracy);
            var microAccuracyAverage = microAccuracyValues.Average();
            var microAccuraciesStdDeviation = CalculateStandardDeviation(microAccuracyValues);
            var microAccuraciesConfidenceInterval95 = CalculateConfidenceInterval95(microAccuracyValues);

            var macroAccuracyValues = metricsInMultipleFolds.Select(m => m.MacroAccuracy);
            var macroAccuracyAverage = macroAccuracyValues.Average();
            var macroAccuraciesStdDeviation = CalculateStandardDeviation(macroAccuracyValues);
            var macroAccuraciesConfidenceInterval95 = CalculateConfidenceInterval95(macroAccuracyValues);

            var logLossValues = metricsInMultipleFolds.Select(m => m.LogLoss);
            var logLossAverage = logLossValues.Average();
            var logLossStdDeviation = CalculateStandardDeviation(logLossValues);
            var logLossConfidenceInterval95 = CalculateConfidenceInterval95(logLossValues);

            var logLossReductionValues = metricsInMultipleFolds.Select(m => m.LogLossReduction);
            var logLossReductionAverage = logLossReductionValues.Average();
            var logLossReductionStdDeviation = CalculateStandardDeviation(logLossReductionValues);
            var logLossReductionConfidenceInterval95 = CalculateConfidenceInterval95(logLossReductionValues);

            _logger.LogInformation($"*************************************************************************************************************");
            _logger.LogInformation($"*       Metrics for Multi-class Classification model      ");
            _logger.LogInformation($"*------------------------------------------------------------------------------------------------------------");
            _logger.LogInformation($"*       Average MicroAccuracy:    {microAccuracyAverage:0.###}  - Standard deviation: ({microAccuraciesStdDeviation:#.###})  - Confidence Interval 95%: ({microAccuraciesConfidenceInterval95:#.###})");
            _logger.LogInformation($"*       Average MacroAccuracy:    {macroAccuracyAverage:0.###}  - Standard deviation: ({macroAccuraciesStdDeviation:#.###})  - Confidence Interval 95%: ({macroAccuraciesConfidenceInterval95:#.###})");
            _logger.LogInformation($"*       Average LogLoss:          {logLossAverage:#.###}  - Standard deviation: ({logLossStdDeviation:#.###})  - Confidence Interval 95%: ({logLossConfidenceInterval95:#.###})");
            _logger.LogInformation($"*       Average LogLossReduction: {logLossReductionAverage:#.###}  - Standard deviation: ({logLossReductionStdDeviation:#.###})  - Confidence Interval 95%: ({logLossReductionConfidenceInterval95:#.###})");
            _logger.LogInformation($"*************************************************************************************************************");

        }

        private static double CalculateStandardDeviation(IEnumerable<double> values)
        {
            double average = values.Average();
            double sumOfSquaresOfDifferences = values.Select(val => (val - average) * (val - average)).Sum();
            double standardDeviation = Math.Sqrt(sumOfSquaresOfDifferences / (values.Count() - 1));
            return standardDeviation;
        }

        private static double CalculateConfidenceInterval95(IEnumerable<double> values)
        {
            double confidenceInterval95 = 1.96 * CalculateStandardDeviation(values) / Math.Sqrt((values.Count() - 1));
            return confidenceInterval95;
        }


    }
}
