using GoWorkFactoryBusinessLogic.BindingModels;
using GoWorkFactoryBusinessLogic.Enums;
using GoWorkFactoryBusinessLogic.Interfaces;
using GoWorkFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoWorkFactoryDataBase.Implementations
{
    public class UserLogic : IUserLogic
    {
        public void ChangeRole(ChangeUserRoleBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                var user = context.Users.FirstOrDefault(x => x.Id == model.UserId);
                if (user == null)
                {
                    throw new Exception("Такого пользователя не существует");
                }

                user.Role = model.Role;
                context.SaveChanges();
            }
        }

        public void CreateOrUpdate(UserBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                var user = context.Users.FirstOrDefault(x => x.Username == model.Username && x.Id != model.Id);
                if (user != null)
                {
                    throw new Exception("Пользователь с таким ником уже существует");
                }
                user = context.Users.FirstOrDefault(x => x.Id == model.Id);
                if (user == null)
                {
                    user = new Models.User
                    {
                        Role = UserRole.User
                    };
                    context.Users.Add(user);
                }

                user.Username = model.Username;
                user.Password = model.Password;
                user.Email = model.Email;
                context.SaveChanges();
            }
        }

        public IEnumerable<UserViewModel> Read(UserBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                return context.Users
                    .Where(x => 
                        (model == null) || 
                        (x.Id == model.Id) ||
                        (x.Username == model.Username && x.Password == model.Password)
                    )
                    .Select(x => new UserViewModel
                    {
                        Id = x.Id,
                        Username = x.Username,
                        Password = x.Password,
                        Email = x.Email,
                        Role = x.Role
                    })
                    .ToList();
            }
        }

        public void Remove(UserBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                var user = context.Users.FirstOrDefault(x => x.Id == model.Id);
                if (user == null)
                {
                    throw new Exception("Такого пользователя не существует");
                }

                context.Users.Remove(user);
                context.SaveChanges();
            }
        }
    }
}
