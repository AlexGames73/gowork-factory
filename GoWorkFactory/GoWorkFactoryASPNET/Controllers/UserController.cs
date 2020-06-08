using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GoWorkFactoryASPNET.Models;
using GoWorkFactoryBusinessLogic.BindingModels;
using GoWorkFactoryBusinessLogic.BusinessLogics;
using GoWorkFactoryBusinessLogic.HelperModels;
using GoWorkFactoryBusinessLogic.Interfaces;
using GoWorkFactoryBusinessLogic.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GoWorkFactoryASPNET.Controllers
{
    public class UserController : Controller
    {
        private IUserLogic userLogic;
        private MailSettings mailSettings;

        public UserController(IUserLogic userLogic, IOptions<MailSettings> mailSettings)
        {
            this.userLogic = userLogic;
            this.mailSettings = mailSettings.Value;
            MailLogic.MailConfig(this.mailSettings);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = userLogic.Read(new UserBindingModel { 
                    Username = model.Username, 
                    Password = model.Password 
                }).FirstOrDefault();

                if (!user.EmailConfirmed)
                {
                    return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { Title = "Ошибка", Message = "Вы не подтвердили почту" });
                }

                if (user != null)
                {
                    await Authenticate(user.Username, user.Id, user.Email); // аутентификация
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { Title = "Ошибка", Message = "Неправильный логин или пароль" });
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string token = Guid.NewGuid().ToString("N");
                    while (userLogic.Read(new UserBindingModel { EmailToken = token }).Count() > 0)
                    {
                        token = Guid.NewGuid().ToString("N");
                    }

                    userLogic.CreateOrUpdate(new UserBindingModel
                    {
                        Username = model.Username,
                        Password = model.Password,
                        Email = model.Email,
                        EmailConfirmed = false,
                        EmailToken = token,
                        Role = GoWorkFactoryBusinessLogic.Enums.UserRole.User
                    });

                    MailLogic.MailSendAsync(new MailSendInfo
                    {
                        MailAddress = model.Email,
                        Subject = "Подтверждение почты",
                        Text = $"Ссылка для подтверждения: https://{HttpContext.Request.Host.Value}/User/EmailConfirm?token={token}"
                    });

                    return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { Title = "Информация", Message = "Подтвердите почту" });
                }
                catch (Exception e)
                {
                    return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { Title = "Ошибка", Message = e.Message });
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult EmailConfirm(string token = "")
        {
            var user = userLogic.Read(new UserBindingModel { EmailToken = token }).FirstOrDefault();
            if (user == null)
            {
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { Title = "Ошибка", Message = "Токен не действителен" });
            }
            if (user.EmailConfirmed)
            {
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { Title = "Ошибка", Message = "Почта уже подтверждена" });
            }

            userLogic.CreateOrUpdate(new UserBindingModel
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                Username = user.Username,
                Role = user.Role,
                EmailToken = "",
                EmailConfirmed = true
            });

            return Redirect("Login");
        }

        private async Task Authenticate(string userName, int userId, string email)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, email)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User");
        }
    }
}