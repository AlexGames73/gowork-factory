using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoWorkFactoryBusinessLogic.BindingModels;
using GoWorkFactoryBusinessLogic.Interfaces;
using GoWorkFactoryBusinessLogic.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public IActionResult DeleteOrder(int orderId)
        {
            orderLogic.Remove(new OrderBindingModel
            {
                Id = orderId
            });
            return Redirect("Index");
        }

        public IActionResult CreateOrder()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            string guid = Guid.NewGuid().ToString();
            while (orderLogic.Read(new OrderBindingModel { SerialNumber = guid }).Count() > 0)
                guid = Guid.NewGuid().ToString();
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                UserId = userId,
                SerialNumber = guid,
                DeliveryDate = DateTime.Now,
                DeliveryAddress = "Address"
            });
            ViewBag.Order = orderLogic.Read(new OrderBindingModel { SerialNumber = guid }).FirstOrDefault();
            return View();
        }

        public IActionResult CreateOrderForm(OrderViewModel order)
        {
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                Id = order.Id,
                SerialNumber = order.SerialNumber,
                UserId = order.UserId,
                DeliveryDate = order.DeliveryDate,
                DeliveryAddress = order.DeliveryAddress
            });
            return Redirect("Index");
        }

        public IActionResult EditOrder(int orderId)
        {
            ViewBag.Order = orderLogic.Read(new OrderBindingModel { Id = orderId }).FirstOrDefault();
            return View("CreateOrder");
        }

        public IActionResult SelectProducts(int orderId)
        {
            ViewBag.AllProducts = productLogic.Read(null);
            ViewBag.Order = orderLogic.Read(new OrderBindingModel { Id = orderId }).FirstOrDefault();
            return View();
        }

        public IActionResult AddToOrder(ChangeProductOrderBindingModel model)
        {
            orderLogic.AddProduct(model);
            return RedirectToAction("SelectProducts", new { orderId = model.OrderId });
        }

        public IActionResult RemoveFromOrder(ChangeProductOrderBindingModel model)
        {
            orderLogic.RemoveProduct(model);
            return RedirectToAction("SelectProducts", new { orderId = model.OrderId });
        }
    }
}