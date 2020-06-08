using System.ComponentModel.DataAnnotations;

namespace GoWorkFactoryBusinessLogic.ViewModels
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "Не введен логин")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Не введен пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
