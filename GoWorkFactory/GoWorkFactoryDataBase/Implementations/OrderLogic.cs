using GoWorkFactoryBusinessLogic.BindingModels;
using GoWorkFactoryBusinessLogic.Interfaces;
using GoWorkFactoryBusinessLogic.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoWorkFactoryDataBase.Implementations
{
    public class OrderLogic : IOrderLogic
    {
        public void AddProduct(ChangeProductOrderBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                var productOrder = context.ProductOrders.FirstOrDefault(x => x.OrderId == model.OrderId && x.ProductId == model.ProductId);
                if (productOrder == null)
                {
                    productOrder = new Models.ProductOrder
                    {
                        OrderId = model.OrderId,
                        ProductId = model.ProductId,
                        ProductAmount = 0
                    };
                    context.ProductOrders.Add(productOrder);
                }

                productOrder.ProductAmount += model.ProductAmount;
                context.SaveChanges();
            }
        }

        public void Create(OrderBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                context.Orders.Add(new Models.Order
                {
                    SerialNumber = model.SerialNumber,
                    DeliveryDate = model.DeliveryDate,
                    DeliveryAddress = model.DeliveryAddress,
                    UserId = model.UserId
                });
                context.SaveChanges();
            }
        }

        public IEnumerable<OrderViewModel> Read(OrderBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                return context.Orders
                    .Include(x => x.User)
                    .Include(x => x.ProductOrders)
                        .ThenInclude(x => x.Product)
                    .Where(x => model == null && x.Id == model.Id)
                    .Select(x => new OrderViewModel
                    {
                        Id = x.Id,
                        SerialNumber = x.SerialNumber,
                        DeliveryDate = x.DeliveryDate,
                        DeliveryAddress = x.DeliveryAddress,
                        UserId = x.UserId,
                        Username = x.User.Username,
                        Products = x.ProductOrders.ToDictionary(y => y.ProductId, y => new Tuple<string, int> (y.Product.Name, y.ProductAmount).ToValueTuple())
                    });
            }
        }

        public void Remove(OrderBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                var order = context.Orders.FirstOrDefault(x => x.Id == model.Id);
                if (order == null)
                {
                    throw new Exception("Такого заказа нет");
                }

                context.Orders.Remove(order);
                context.SaveChanges();
            }
        }

        public void RemoveProduct(ChangeProductOrderBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                var productOrder = context.ProductOrders.FirstOrDefault(x => x.OrderId == model.OrderId && x.ProductId == model.ProductId);
                if (productOrder == null || productOrder.ProductAmount < model.ProductAmount)
                {
                    throw new Exception("В заказе не существует столько товаров");
                }

                productOrder.ProductAmount -= model.ProductAmount;
                context.SaveChanges();
            }
        }

        public void Update(OrderBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                var order = context.Orders.FirstOrDefault(x => x.Id == model.Id);
                if (order == null)
                {
                    throw new Exception("Такого заказа не существует");
                }

                order.SerialNumber = model.SerialNumber;
                order.UserId = model.UserId;
                order.DeliveryDate = model.DeliveryDate;
                order.DeliveryAddress = model.DeliveryAddress;
                context.SaveChanges();
            }
        }
    }
}
