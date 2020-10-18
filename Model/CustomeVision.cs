﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    public class CustomeVision
    {

        public class ToyModel
        {
            public string id { get; set; }
            public string project { get; set; }
            public string iteration { get; set; }
            public DateTime created { get; set; }
            public Prediction[] predictions { get; set; }
        }

        public class Prediction
        {
            public float probability { get; set; }
            public string tagId { get; set; }
            public string tagName { get; set; }
        }

    }
}
