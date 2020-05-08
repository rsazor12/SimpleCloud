using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCloudMonolithic.Application.Common.Interfaces
{
    public interface IModelBuilder
    {
        (MLContext, ITransformer, DataViewSchema) CreateModel(string trainDataFilePath);
        void SaveModel(MLContext mlContext, ITransformer mlModel, string modelRelativePath, DataViewSchema modelInputSchema);
    }
}
