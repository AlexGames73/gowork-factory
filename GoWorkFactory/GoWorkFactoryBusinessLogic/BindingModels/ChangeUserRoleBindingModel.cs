using GoWorkFactoryBusinessLogic.Enums;

namespace GoWorkFactoryBusinessLogic.BindingModels
{
    public class ChangeUserRoleBindingModel
    {
        public int UserId { get; set; }
        public UserRole Role { get; set; }
    }
}
