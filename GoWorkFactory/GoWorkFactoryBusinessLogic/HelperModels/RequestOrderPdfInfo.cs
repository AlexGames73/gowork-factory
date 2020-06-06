using GoWorkFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoWorkFactoryBusinessLogic.HelperModels
{
    public class RequestOrderPdfInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<ReportRequestOrderViewModel> ReportRequestOrders { get; set; }
    }
}
