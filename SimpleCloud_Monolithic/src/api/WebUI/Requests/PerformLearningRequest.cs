using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCloud_Monolithic.WebUI.Requests
{
    [BindProperties]
    public class PerformLearningRequest
    {
        [FromQuery]
        public string mlServiceId { get; set; }
    }
}
