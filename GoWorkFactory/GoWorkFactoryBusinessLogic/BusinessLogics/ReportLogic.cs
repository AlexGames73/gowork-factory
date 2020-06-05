using GoWorkFactoryBusinessLogic.BindingModels;
using GoWorkFactoryBusinessLogic.HelperModels;
using GoWorkFactoryBusinessLogic.Interfaces;
using GoWorkFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GoWorkFactoryBusinessLogic.BusinessLogics
{
    public class ReportLogic
    {
        private readonly IOrderLogic orderLogic;

        public ReportLogic(IOrderLogic orderLogic)
        {
            this.orderLogic = orderLogic;
        }

        public List<IGrouping<string, ReportOrdersProductsViewModel>> GetOrdersProducts(int userId)
        {
            return orderLogic.Read(new OrderBindingModel { UserId = userId })
                .SelectMany(x => x.Products.Select(y => new ReportOrdersProductsViewModel
                {
                    OrderSerialNumber = x.SerialNumber,
                    ProductName = y.Name,
                    Count = y.Count,
                    Price = y.Price
                }))
                .GroupBy(x => x.OrderSerialNumber)
                .ToList();
        }

        public Stream SaveOrdersProductsToWordFile(int userId)
        {
            return SaveToWord.CreateDocOrdersProducts(new OrdersProductsWordInfo
            {
                Title = "Отчет по заказам",
                OrdersProducts = GetOrdersProducts(userId)
            });
        }

        public Stream SaveOrdersProductsToExcelFile(int userId)
        {
            return SaveToExcel.CreateDocOrdersProducts(new OrdersProductsExcelInfo
            {
                Title = "Отчет по заказам",
                OrdersProducts = GetOrdersProducts(userId)
            });
        }
    }
}
