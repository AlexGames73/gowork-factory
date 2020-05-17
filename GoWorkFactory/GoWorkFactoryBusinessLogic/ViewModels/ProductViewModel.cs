using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GoWorkFactoryBusinessLogic.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название продукта")]
        public string NameProduct { get; set; }

        [DisplayName("Стоимость продукта")]
        public int CostProduct { get; set; }
        public Dictionary<int, (string, int)> Materials { get; set; }
    }
}
