using System;
using System.Collections.Generic;

namespace GoWorkFactoryBusinessLogic.ViewModels
{
    public class ReportRequestOrderViewModel
    {
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public List<string> NameMaterial { get; set; }
        public List<int> Count { get; set; }
        public List<int> Price { get; set; }
    }
}
