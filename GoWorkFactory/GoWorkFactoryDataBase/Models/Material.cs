using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoWorkFactoryDataBase.Models
{
    public class Material
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }

        [ForeignKey("MaterialId")]
        public virtual List<MaterialProduct> MaterialProducts { get; set; }

        [ForeignKey("MaterialId")]
        public virtual List<MaterialRequest> MaterialRequests { get; set; }
    }
}
