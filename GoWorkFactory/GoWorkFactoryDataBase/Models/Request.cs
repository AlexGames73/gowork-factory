using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoWorkFactoryDataBase.Models
{
    public class Request
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime Date { get; set; }
        
        [ForeignKey("RequestId")]
        public virtual List<MaterialRequest> MaterialRequests { get; set; }
    }
}
