using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MachineLearning_SimpleCloud_MicroservicesHttp.Application.Models
{
    public class ModelOutput
    {
        // ColumnName attribute is used to change the column name from
        // its default value, which is the name of the field.
        [ColumnName("PredictedLabel")]
        public String Prediction { get; set; }
        [ColumnName("Score")]
        public float[] Score { get; set; }

    }
}
