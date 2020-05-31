using System;

namespace GoWorkFactoryBusinessLogic.BindingModels
{
    public class OrderBindingModel
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public string SerialNumber { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string DeliveryAddress { get; set; }
        public bool Reserved { get; set; }
    }
}
