using GoWorkFactoryBusinessLogic.Enums;
using System;

namespace GoWorkFactoryBusinessLogic.ViewModels
{
    public class ReportProductsViewModel
    {
        public DateTime DeliveryDate { get; set; }
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }
        public int TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
    }
}
