using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GoWorkFactoryDataBase.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        [ForeignKey("ProductId")]
        public List<MaterialProduct> MaterialProducts { get; set; }
    }
}
