using GoWorkFactoryBusinessLogic.BindingModels;
using GoWorkFactoryBusinessLogic.Enums;
using GoWorkFactoryBusinessLogic.Interfaces;
using GoWorkFactoryBusinessLogic.ViewModels;
using GoWorkFactoryDataBase.Models;
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
                    productOrder = new ProductOrder
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

        public int CreateOrUpdate(OrderBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                var order = context.Orders
                    .Include(x => x.User)
                    .Include(x => x.ProductOrders)
                        .ThenInclude(x => x.Product)
                    .FirstOrDefault(x => x.Id == model.Id);
                if (order == null)
                {
                    order = new Order();
                    context.Orders.Add(order);
                }

                order.UserId = model.UserId.Value;
                order.DeliveryDate = model.DeliveryDate;
                order.DeliveryAddress = model.DeliveryAddress;
                order.Status = model.Status;
                context.SaveChanges();

                return order.Id;
            }
        }

        public IEnumerable<OrderViewModel> Read(OrderBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                return context.Orders
                    .Include(x => x.ProductOrders)
                    .ThenInclude(x => x.Product)
                    .Include(x => x.User)
                    .Where(x => model == null || x.Id == model.Id || x.UserId == model.UserId
                    || (model.DateFrom.HasValue && model.DateTo.HasValue && x.DeliveryDate >= model.DateFrom && x.DeliveryDate <= model.DateTo))
                    .Select(GetViewModel)
                    .ToList();
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

        public void ReservationOrder(ReservationBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var order = context.Orders
                            .Include(x => x.ProductOrders)
                                .ThenInclude(x => x.Product)
                                    .ThenInclude(x => x.MaterialProducts)
                                        .ThenInclude(x => x.Material)
                            .FirstOrDefault(x => x.Id == model.OrderId);
                        if (order == null)
                        {
                            throw new Exception("Такого заказа не существует");
                        }

                        order.Status = model.Reserved ? OrderStatus.Зарезервирован : OrderStatus.Создан;

                        foreach (var product in order.ProductOrders)
                        {
                            foreach (var materialProduct in product.Product.MaterialProducts)
                            {
                                materialProduct.Material.Count -= materialProduct.MaterialAmount;

                                if (materialProduct.Material.Count < 0)
                                    throw new Exception("Недостаточно материалов на складах");
                            }
                        }

                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public OrderViewModel GetViewModel(Order order)
        {
            return new OrderViewModel
            {
                DeliveryAddress = order.DeliveryAddress,
                DeliveryDate = order.DeliveryDate,
                Id = order.Id,
                Products = order.ProductOrders.Select(y => new ProductViewModel
                {
                    Id = y.Product.Id,
                    Name = y.Product.Name,
                    Price = y.Product.Price,
                    Count = y.ProductAmount
                }).ToList(),
                UserId = order.UserId,
                Username = order.User?.Username,
                Status = order.Status
            };
        }
    }
}
