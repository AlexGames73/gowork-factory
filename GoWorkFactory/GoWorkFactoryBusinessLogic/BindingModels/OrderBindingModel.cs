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
        public bool Reserved { get; set; }
        public OrderStatus Status { get; set; }

        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
