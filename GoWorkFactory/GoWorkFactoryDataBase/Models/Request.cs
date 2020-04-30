using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GoWorkFactoryDataBase.Models
{
    public class Request
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        
        [ForeignKey("RequestId")]
        public List<MaterialRequest> MaterialRequests { get; set; }
    }
}
