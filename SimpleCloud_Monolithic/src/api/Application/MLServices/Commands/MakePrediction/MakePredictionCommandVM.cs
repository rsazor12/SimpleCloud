using SimpleCloud_Monolithic.Application.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCloud_Monolithic.Application.MLServices.Commands.MakePrediction
{
    public class MakePredictionCommandVM
    {
        public string FileName { get; set; }
        public ModelOutputDTO ModelOutputDTO { get; set; }
    }
}
