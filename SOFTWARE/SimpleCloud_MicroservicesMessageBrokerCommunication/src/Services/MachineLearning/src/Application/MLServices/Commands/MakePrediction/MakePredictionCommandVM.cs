﻿using MachineLearning_SimpleCloud_Broker.Application.Models.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MachineLearning_SimpleCloud_Broker.Application.MLServices.Commands.MakePrediction
{
    public class MakePredictionCommandVM
    {
        public string FileName { get; set; }
        public ModelOutputDTO ModelOutputDTO { get; set; }
    }
}