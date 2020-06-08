using System.ComponentModel.DataAnnotations;

namespace GoWorkFactoryBusinessLogic.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "Не введен логин")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Не введен пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Не введен email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
