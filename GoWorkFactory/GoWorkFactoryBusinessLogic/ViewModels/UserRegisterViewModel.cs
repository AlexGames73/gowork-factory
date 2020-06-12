using System.ComponentModel.DataAnnotations;

namespace GoWorkFactoryBusinessLogic.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "Не введен логин")]
        [MinLength(8, ErrorMessage = "Логин слишком короткий")]
        [MaxLength(20, ErrorMessage = "Логин слишком длинный")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Не введен пароль")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Пароль слишком короткий")]
        [MaxLength(16, ErrorMessage = "Пароль слишком длинный")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Не введен email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Не правильно введен email")]
        public string Email { get; set; }

        public string Error { get; set; }
    }
}
