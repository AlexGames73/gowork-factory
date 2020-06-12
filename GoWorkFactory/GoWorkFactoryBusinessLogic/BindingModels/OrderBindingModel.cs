using GoWorkFactoryBusinessLogic.Enums;
using System;

namespace GoWorkFactoryBusinessLogic.BindingModels
{
    public class OrderBindingModel
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string DeliveryAddress { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
