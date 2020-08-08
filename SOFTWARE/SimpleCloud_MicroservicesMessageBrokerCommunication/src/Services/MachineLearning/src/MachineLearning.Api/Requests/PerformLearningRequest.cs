using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachineLearning_SimpleCloud_Broker.Requests
{
    [BindProperties]
    public class PerformLearningRequest
    {
        [FromQuery]
        public string MlServiceId { get; set; }
    }
}
