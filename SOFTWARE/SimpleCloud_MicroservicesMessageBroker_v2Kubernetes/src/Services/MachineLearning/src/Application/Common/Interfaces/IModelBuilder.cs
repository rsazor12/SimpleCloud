using Microsoft.ML;
using MachineLearning_SimpleCloud_MicroservicesHttp.Application.Models;
using MachineLearning_SimpleCloud_MicroservicesHttp.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MachineLearning_SimpleCloud_MicroservicesHttp.Application.Common.Interfaces
{
    public interface IModelBuilder
    {
        (MLContext, ITransformer, DataViewSchema) CreateModel(IEnumerable<ModelInput> modelInput);
        void SaveModel(MLContext mlContext, ITransformer mlModel, string modelRelativePath, DataViewSchema modelInputSchema);

        public IEnumerable<ModelOutput> Predict(string modelPath, IEnumerable<ModelInput> input);
    }
}
