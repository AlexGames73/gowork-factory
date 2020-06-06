using GoWorkFactoryBusinessLogic.BindingModels;
using GoWorkFactoryBusinessLogic.Enums;
using GoWorkFactoryBusinessLogic.Interfaces;
using GoWorkFactoryBusinessLogic.ViewModels;
using GoWorkFactoryDataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoWorkFactoryDataBase.Implementations
{
    public class UserLogic : IUserLogic
    {
        public UserViewModel CreateOrUpdate(UserBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                var user = context.Users.FirstOrDefault(x => (x.Username == model.Username || x.Email == model.Email) && x.Id != model.Id);
                if (user != null)
                {
                    throw new Exception("Пользователь с таким ником или почтой уже существует");
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
                user.EmailToken = model.EmailToken;
                user.EmailConfirmed = model.EmailConfirmed;
                user.Role = model.Role;
                context.SaveChanges();
                return GetViewModel(user);
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
                        (x.Username == model.Username && x.Password == model.Password) ||
                        (x.EmailToken == model.EmailToken)
                    )
                    .Select(GetViewModel)
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

        private UserViewModel GetViewModel(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                Role = user.Role,
                EmailConfirmed = user.EmailConfirmed,
                EmailToken = user.EmailToken
            };
        }
    }
}
