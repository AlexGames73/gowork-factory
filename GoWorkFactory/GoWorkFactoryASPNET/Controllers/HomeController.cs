using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office.CustomUI;
using GoWorkFactoryBusinessLogic.BindingModels;
using GoWorkFactoryBusinessLogic.BusinessLogics;
using GoWorkFactoryBusinessLogic.Enums;
using GoWorkFactoryBusinessLogic.HelperModels;
using GoWorkFactoryBusinessLogic.Interfaces;
using GoWorkFactoryBusinessLogic.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using SautinSoft;

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
            ViewBag.Order = orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                UserId = userId,
                DeliveryDate = DateTime.Now,
                DeliveryAddress = "Address",
                Status = OrderStatus.Создан,
                Reserved = false
            });
            return View();
        }

        public IActionResult CreateOrderForm(OrderViewModel order)
        {
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                Id = order.Id,
                UserId = order.UserId,
                DeliveryDate = order.DeliveryDate,
                DeliveryAddress = order.DeliveryAddress,
                Status = OrderStatus.Создан,
                Reserved = false
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
                        FileData = reportLogic.SaveOrdersProductsToExcelFile(userId),
                        Name = "ОтчетПоЗаказам"
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
                        FileData = reportLogic.SaveOrdersProductsToWordFile(userId),
                        Name = "ОтчетПоЗаказам"
                    }
                }
            });
            return Ok();
        }

        public IActionResult OrderReport()
        {
            return View();
        }

        [HttpPost]
        [Obsolete]
        public IActionResult OrderReportModel(OrderReportViewModel model)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            string email = User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.Email).Value;
            Stream pdfFile = reportLogic.SaveProductsToPdfFile(new ReportProductsBindingModel
            {
                From = model.From,
                To = model.To,
                UserId = userId
            });
            if (!model.IsEmail)
            {
                byte[] data = new byte[pdfFile.Length];
                pdfFile.Read(data);
                string base64 = Convert.ToBase64String(data);
                return Content($"<object data=\"data:application/pdf;base64,{base64}\" type=\"application/pdf\" width=\"100%\" height=\"1200px\" />", "text/html");
            }
            MailLogic.MailSendAsync(new MailSendInfo
            {
                MailAddress = email,
                Subject = "Отчет по товарам",
                Text = "Доброго времени суток, Ваш заказанный отчет прикреплен к этому сообщению",
                Attachments = new List<MailAttachment>
                {
                    new MailAttachment
                    {
                        ContentType = MimeTypes.Pdf,
                        FileData = pdfFile,
                        Name = "ОтчетПоТоварам"
                    }
                }
            });
            return Ok();
        }
    }
}