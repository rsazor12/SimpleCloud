using MachineLearning_SimpleCloud_MicroservicesHttp.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace MachineLearning_SimpleCloud_MicroservicesHttp.Application.Models.DTO
{
    public class ModelOutputDTO : IMapFrom<ModelOutput>
    {
        public string Prediction { get; set; }
        public float[] Score { get; set; }
    }
}
