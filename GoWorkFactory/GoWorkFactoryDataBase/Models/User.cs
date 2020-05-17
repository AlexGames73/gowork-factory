using GoWorkFactoryBusinessLogic.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoWorkFactoryDataBase.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }

        [ForeignKey("UserId")]
        public virtual List<Request> Requests { get; set; }
        [ForeignKey("UserId")]
        public virtual List<Order> Orders { get; set; }
    }
}
