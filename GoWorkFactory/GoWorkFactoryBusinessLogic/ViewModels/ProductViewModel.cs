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
        public string Name { get; set; }

        [DisplayName("Стоимость продукта")]
        public int Price { get; set; }

        [DisplayName("Стоимость продукта")]
        public int Count { get; set; }
        public Dictionary<int, (string, int)> Materials { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
