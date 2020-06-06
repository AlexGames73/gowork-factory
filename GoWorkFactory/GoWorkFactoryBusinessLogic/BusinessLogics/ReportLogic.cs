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
        private readonly IProductLogic productLogic;

        public ReportLogic(IOrderLogic orderLogic, IRequestLogic requestLogic, IProductLogic productLogic)
        {
            this.orderLogic = orderLogic;
            this.requestLogic = requestLogic;
            this.productLogic = productLogic;
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

        public List<ReportProductsViewModel> GetProducts(ReportProductsBindingModel model)
        {
            return orderLogic.Read(new OrderBindingModel { UserId = model.UserId, DateFrom = model.From, DateTo = model.To })
                .SelectMany(x => x.Products.Select(y => new ReportProductsViewModel
                {
                    OrderId = x.Id,
                    Status = x.Status,
                    Count = y.Count,
                    DeliveryDate = x.DeliveryDate,
                    ProductName = y.Name,
                    TotalPrice = y.Price * y.Count
                }))
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
                NameMaterial = x.Products.SelectMany(y => productLogic.Read(new ProductBindingModel { Id = y.Id }))
                .SelectMany(y => y.Materials.Values)
                .Select(y => (y.Item1, y.Item2)).GroupBy(y => y.Item1).Select(y => y.Select(p => p.Item1).FirstOrDefault()).ToList(),
                Count = x.Products.SelectMany(y => productLogic.Read(new ProductBindingModel { Id = y.Id }))
                .SelectMany(y => y.Materials.Values)
                .Select(y => (y.Item1, y.Item2)).GroupBy(y => y.Item1).Select(y => y.Sum(p => p.Item2)).ToList(),
                Price = x.Products.Select(y => y.Price).ToList()
            });

            reportRequestOrders.AddRange(orders);

            return reportRequestOrders;
        }

        [Obsolete]
        public Stream SaveProductsToPdfFile(ReportProductsBindingModel model)
        {
            return SaveToPdf.CreateDocProducts(new ProductsPdfInfo
            {
                From = model.From,
                To = model.To,
                Products = GetProducts(model),
                Title = "Отчет по товарам"
            });
        }
        [Obsolete]
        public Stream SaveRequestOrderToPdfFile(DateTime dateTo, DateTime dateFrom)
        {
            return SaveToPdf.CreateDocRequestOrder(new RequestOrderPdfInfo
            {
                DateFrom = dateFrom,
                DateTo = dateTo,
                FileName = "Отчет по заявкам и заказам",
                ReportRequestOrders = GetRequestsOrders(dateTo, dateFrom),
                Title = "Отчет по заявкам и заказам"
            });
        }
    }
}
