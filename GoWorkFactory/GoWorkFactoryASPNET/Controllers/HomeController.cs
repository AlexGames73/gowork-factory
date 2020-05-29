using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoWorkFactoryBusinessLogic.BindingModels;
using GoWorkFactoryBusinessLogic.Interfaces;
using GoWorkFactoryBusinessLogic.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoWorkFactoryASPNET.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IProductLogic productLogic;
        private IOrderLogic orderLogic;

        public HomeController(IProductLogic productLogic, IOrderLogic orderLogic)
        {
            this.productLogic = productLogic;
            this.orderLogic = orderLogic;
        }

        public IActionResult Index()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            return View(orderLogic.Read(new OrderBindingModel { UserId = userId }).ToList());
        }

        public IActionResult AddToCart(int productId)
        {
            return View("Index");
        }
    }
}