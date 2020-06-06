using GoWorkFactoryBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoWorkFactoryDataBase.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string DeliveryAddress { get; set; }
        public bool Reserved { get; set; }
        public OrderStatus Status { get; set; }

        [ForeignKey("OrderId")]
        public List<ProductOrder> ProductOrders { get; set; }
    }
}
