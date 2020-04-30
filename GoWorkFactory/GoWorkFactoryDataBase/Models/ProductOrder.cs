using System;
using System.Collections.Generic;
using System.Text;

namespace GoWorkFactoryDataBase.Models
{
    public class ProductOrder
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int ProductAmount { get; set; }
    }
}
