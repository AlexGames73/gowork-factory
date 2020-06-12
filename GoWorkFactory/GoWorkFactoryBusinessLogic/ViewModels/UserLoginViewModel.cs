using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GoWorkFactoryBusinessLogic.ViewModels
{
    public class UserLoginViewModel
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

        public string Error { get; set; }
    }
}
