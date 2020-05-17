using GoWorkFactoryBusinessLogic.BindingModels;
using GoWorkFactoryBusinessLogic.ViewModels;

namespace GoWorkFactoryBusinessLogic.Interfaces
{
    public interface IUserLogic : ILogic<UserBindingModel, UserViewModel>
    {
        void ChangeRole(ChangeUserRoleBindingModel model);
    }
}
