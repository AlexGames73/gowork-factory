﻿using System.Collections.Generic;

namespace GoWorkFactoryBusinessLogic.BindingModels
{
    public class ProductBindingModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public Dictionary<int, (string, int)> Materials { get; set; }
    }
}
