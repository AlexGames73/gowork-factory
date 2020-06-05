using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoWorkFactoryBusinessLogic.BindingModels;
using GoWorkFactoryBusinessLogic.BusinessLogics;
using GoWorkFactoryBusinessLogic.HelperModels;
using GoWorkFactoryBusinessLogic.Interfaces;
using GoWorkFactoryBusinessLogic.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace GoWorkFactoryASPNET.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IProductLogic productLogic;
        private IOrderLogic orderLogic;
        private ReportLogic reportLogic;

        public HomeController(IProductLogic productLogic, IOrderLogic orderLogic, ReportLogic reportLogic)
        {
            this.productLogic = productLogic;
            this.orderLogic = orderLogic;
            this.reportLogic = reportLogic;
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

        public IActionResult ReserveOrders(List<ReserveViewModel> reserveViewModels)
        {
            foreach (var item in reserveViewModels)
            {
                orderLogic.ReservationOrder(new ReservationBindingModel
                {
                    OrderId = item.OrderId,
                    Reserved = item.Reserved
                });
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult ExcelReport()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            MailLogic.MailSendAsync(new MailSendInfo
            {
                MailAddress = User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.Email).Value,
                Subject = "Отчет по заказам",
                Text = "Доброго времени суток, Ваш заказанный отчет прикреплен к этому сообщению",
                Attachments = new List<MailAttachment>
                {
                    new MailAttachment
                    {
                        ContentType = MimeTypes.Excel,
                        FileData = reportLogic.SaveOrdersProductsToExcelFile(userId)
                    }
                }
            });
            return Ok();
        }

        [HttpPost]
        public IActionResult WordReport()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            MailLogic.MailSendAsync(new MailSendInfo
            {
                MailAddress = User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.Email).Value,
                Subject = "Отчет по заказам",
                Text = "Доброго времени суток, Ваш заказанный отчет прикреплен к этому сообщению",
                Attachments = new List<MailAttachment>
                {
                    new MailAttachment
                    {
                        ContentType = MimeTypes.Word,
                        FileData = reportLogic.SaveOrdersProductsToWordFile(userId)
                    }
                }
            });
            return Ok();
        }
    }
}