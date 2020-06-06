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

        public List<IGrouping<int, ReportOrdersProductsViewModel>> GetOrdersProducts(int userId)
        {
            return orderLogic.Read(new OrderBindingModel { UserId = userId })
                .SelectMany(x => x.Products.Select(y => new ReportOrdersProductsViewModel
                {
                    OrderId = x.Id,
                    ProductName = y.Name,
                    Count = y.Count,
                    Price = y.Price
                }))
                .GroupBy(x => x.OrderId)
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

        public Stream SaveRequestMaterialsToDocFile(List<(string, int)> materials)
        {
            return SaveToWord.CreateDocRequestMaterials(new RequestMaterialsInfo
            {
                Title = "Заявка на материалы",
                Materials = materials
            });
        }
        public Stream SaveRequestMaterialsToExcelFile(List<(string, int)> materials)
        {
            return SaveToExcel.CreateDocRequestMaterials(new RequestMaterialsInfo
            {
                Title = "Заявка на материалы",
                Materials = materials
            });
        }
    }
}
