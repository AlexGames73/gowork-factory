using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoWorkFactoryDataBase.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        [ForeignKey("ProductId")]
        public virtual List<MaterialProduct> MaterialProducts { get; set; }
        [ForeignKey("ProductId")]
        public virtual List<ProductOrder> ProductOrders { get; set; }
    }
}
