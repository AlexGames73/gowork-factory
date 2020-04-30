using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GoWorkFactoryDataBase.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string SerialNumber { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string DeliveryAddress { get; set; }

        [ForeignKey("OrderId")]
        public virtual List<ProductOrder> ProductOrders { get; set; }
    }
}
