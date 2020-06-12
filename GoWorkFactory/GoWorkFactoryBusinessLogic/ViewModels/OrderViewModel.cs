using GoWorkFactoryBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GoWorkFactoryBusinessLogic.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [DisplayName("Пользователь")]
        [DataType(DataType.Text)]
        public string Username { get; set; }
        [DisplayName("Дата доставки")]
        [DataType(DataType.Date)]
        public DateTime DeliveryDate { get; set; }
        [DisplayName("Адрес доставки")]
        [DataType(DataType.Text)]
        public string DeliveryAddress { get; set; }
        [DisplayName("Статус заказа")]
        public OrderStatus Status { get; set; }

        public List<ProductViewModel> Products { get; set; }
    }
}
