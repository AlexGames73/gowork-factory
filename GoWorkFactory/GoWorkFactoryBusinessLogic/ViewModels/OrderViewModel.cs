using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GoWorkFactoryBusinessLogic.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [DisplayName("Пользователь")]
        public string Username { get; set; }
        [DisplayName("Номер заказа")]
        public string SerialNumber { get; set; }
        [DisplayName("Дата доставки")]
        public DateTime DeliveryDate { get; set; }
        [DisplayName("Адрес доставки")]
        public string DeliveryAddress { get; set; }

        public List<ProductViewModel> Products { get; set; }
    }
}
