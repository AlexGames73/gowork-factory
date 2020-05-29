﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GoWorkFactoryBusinessLogic.BindingModels
{
    public class RequestBindingModel
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<int, (int, int)> Materials { get; set; }
    }
}