using System;
using System.Collections.Generic;
using System.Text;

namespace GoWorkFactoryBusinessLogic.ViewModels
{
    public class ReportOrdersProductsViewModel
    {
        public string OrderSerialNumber { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
    }
}
