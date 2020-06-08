using System;

namespace GoWorkFactoryBusinessLogic.HelperModels
{
    public class ExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        //public List<IGrouping<DateTime, ReportOrdersViewModel>> Orders { get; set; }
    }
}
