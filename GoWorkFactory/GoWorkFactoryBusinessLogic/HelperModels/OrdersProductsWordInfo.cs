using GoWorkFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoWorkFactoryBusinessLogic.HelperModels
{
    public class OrdersProductsWordInfo
    {
        public string Title { get; set; }
        public List<IGrouping<string, ReportOrdersProductsViewModel>> OrdersProducts { get; set; }
    }
}
