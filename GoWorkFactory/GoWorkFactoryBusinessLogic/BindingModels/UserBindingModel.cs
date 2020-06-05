using GoWorkFactoryBusinessLogic.Enums;
using System;

namespace GoWorkFactoryBusinessLogic.BindingModels
{
    public class UserBindingModel
    {
        public int? Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public string EmailToken { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
