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
        private readonly IRequestLogic requestLogic;

        public ReportLogic(IOrderLogic orderLogic, IRequestLogic requestLogic)
        {
            this.orderLogic = orderLogic;
            this.requestLogic = requestLogic;
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

        public List<ReportRequestOrderViewModel> GetRequestsOrders(DateTime dateTo, DateTime dateFrom)
        {
            List<ReportRequestOrderViewModel> reportRequestOrders = new List<ReportRequestOrderViewModel>();

            var requests = requestLogic.Read(new RequestBindingModel
            {
                DateFrom = dateFrom,
                DateTo = dateTo
            }).Select(x => new ReportRequestOrderViewModel { 
                Date = x.Date,
                Type = "Заявка",
                NameMaterial = x.Materials.Select(y => y.Value.Item1).ToList(),
                Count = x.Materials.Select(y => y.Value.Item2).ToList(),
                Price = x.Materials.Select(y => y.Value.Item3).ToList()
            });

            reportRequestOrders.AddRange(requests);

            var orders = orderLogic.Read(new OrderBindingModel
            {
                DateFrom = dateFrom,
                DateTo = dateTo
            }).Select(x => new ReportRequestOrderViewModel
            {
                Date = x.DeliveryDate,
                Type = "Заказ",
                Count = x.Products.SelectMany(y => y.Materials.Values).Select(y => y.Item2).ToList(),
                NameMaterial = x.Products.SelectMany(y => y.Materials.Values).Select(y => y.Item1).ToList(),
                Price = x.Products.Select(y => y.Price).ToList()
            });

            reportRequestOrders.AddRange(orders);

            return reportRequestOrders;
        }
    }
}
