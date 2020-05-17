using System;
using System.Collections.Generic;
using System.Text;

namespace GoWorkFactoryBusinessLogic.BindingModels
{
    public class ProductBindingModel
    {
        public int? Id { get; set; }
        public string NameProduct { get; set; }
        public int CostProduct { get; set; }
        public Dictionary<int, (string, int)> Materials { get; set; }
    }
}
