using System;
using System.Collections.Generic;

namespace GoWorkFactoryBusinessLogic.BindingModels
{
    public class RequestBindingModel
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<int, (int, int)> Materials { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
