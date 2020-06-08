using GoWorkFactoryBusinessLogic.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace GoWorkFactoryBusinessLogic.HelperModels
{
    public class OrdersProductsWordInfo
    {
        public string Title { get; set; }
        public List<IGrouping<int, ReportOrdersProductsViewModel>> OrdersProducts { get; set; }
    }
}
