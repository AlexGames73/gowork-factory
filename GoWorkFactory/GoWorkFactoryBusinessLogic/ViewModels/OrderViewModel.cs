﻿using GoWorkFactoryBusinessLogic.Enums;
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
        [DisplayName("Дата доставки")]
        public DateTime DeliveryDate { get; set; }
        [DisplayName("Адрес доставки")]
        public string DeliveryAddress { get; set; }
        [DisplayName("Зарезервинован")]
        public bool Reverved { get; set; }
        [DisplayName("Статус заказа")]
        public OrderStatus Status { get; set; }

        public List<ProductViewModel> Products { get; set; }
    }
}
