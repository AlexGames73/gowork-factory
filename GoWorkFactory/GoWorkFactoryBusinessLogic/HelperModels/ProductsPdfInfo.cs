using GoWorkFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoWorkFactoryBusinessLogic.HelperModels
{
    public class ProductsPdfInfo
    {
        public string Title { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public List<ReportProductsViewModel> Products { get; set; }
    }
}
