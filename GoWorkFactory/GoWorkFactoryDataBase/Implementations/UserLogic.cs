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

        public void Create(UserBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                context.Users.Add(new Models.User
                {
                    Username = model.Username,
                    Password = model.Password,
                    Email = model.Email,
                    Role = UserRole.User
                });
                context.SaveChanges();
            }
        }

        public IEnumerable<UserViewModel> Read(UserBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                return context.Users
                    .Where(x => model == null || x.Id == model.Id)
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

        public void Update(UserBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                var user = context.Users.FirstOrDefault(x => x.Id == model.Id);
                if (user == null)
                {
                    throw new Exception("Такого пользователя не существует");
                }

                user.Username = model.Username;
                user.Password = model.Password;
                user.Email = model.Email;
                context.SaveChanges();
            }
        }
    }
}
