using Microsoft.ML;
using SimpleCloud_Monolithic.Application.Models;
using SimpleCloudMonolithic.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCloudMonolithic.Application.Common.Interfaces
{
    public interface IModelBuilder
    {
        (MLContext, ITransformer, DataViewSchema) CreateModel(IEnumerable<ModelInput> modelInput);
        void SaveModel(MLContext mlContext, ITransformer mlModel, string modelRelativePath, DataViewSchema modelInputSchema);

        public IEnumerable<ModelOutput> Predict(string modelPath, IEnumerable<ModelInput> input);
    }
}
