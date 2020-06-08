using System;

namespace GoWorkFactoryBusinessLogic.ViewModels
{
    public class OrderReportViewModel
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool IsEmail { get; set; }
    }
}
