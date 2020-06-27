using SimpleCloudMonolithic.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCloud_Monolithic.Application.Models.DTO
{
    public class ModelOutputDTO : IMapFrom<ModelOutput>
    {
        public string Prediction { get; set; }
        public float[] Score { get; set; }
    }
}
