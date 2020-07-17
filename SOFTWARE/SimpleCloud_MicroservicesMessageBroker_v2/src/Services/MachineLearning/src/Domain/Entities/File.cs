﻿using MachineLearning_SimpleCloud_MicroservicesHttp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MachineLearning_SimpleCloud_MicroservicesHttp.Domain.Entities
{
    public class File: Entity
    {
        public string Name { get; private set; }
        public string Path { get; private set; }

        public File()
        {
        }
        public File(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}
