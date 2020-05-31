using GoWorkFactoryBusinessLogic.BindingModels;
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

        public void CreateOrUpdate(OrderBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                var order = context.Orders.FirstOrDefault(x => x.Id == model.Id);
                if (order == null)
                {
                    order = new Order();
                    context.Orders.Add(order);
                }

                order.SerialNumber = model.SerialNumber;
                order.UserId = model.UserId.Value;
                order.DeliveryDate = model.DeliveryDate;
                order.DeliveryAddress = model.DeliveryAddress;
                context.SaveChanges();
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
                    .Where(x => model == null || x.Id == model.Id || x.UserId == model.UserId || x.SerialNumber == model.SerialNumber)
                    .Select(OrderToViewModel)
                    .ToList();
            }
        }

        private OrderViewModel OrderToViewModel(Order x)
        {
            return new OrderViewModel
            {
                Id = x.Id,
                SerialNumber = x.SerialNumber,
                DeliveryDate = x.DeliveryDate,
                DeliveryAddress = x.DeliveryAddress,
                UserId = x.UserId,
                Username = x.User.Username,
                Products = x.ProductOrders.Select(y => new ProductViewModel
                {
                    Id = y.Product.Id,
                    Name = y.Product.Name,
                    Price = y.Product.Price,
                    Count = y.ProductAmount
                }).ToList()
            };
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
                var order = context.Orders.FirstOrDefault(x => x.Id == model.OrderId);
                if (order == null)
                {
                    throw new Exception("Такого заказа не существует");
                }

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        List<ProductOrder> productOrders = context.ProductOrders.Include(x => x.Product).Include(x => x.Order).ToList();

                        foreach (var product in productOrders)
                        {
                            List<MaterialProduct> materialProducts = context.MaterialProducts.Include(x => x.Material).ToList();

                            foreach (var material in materialProducts)
                            {
                                material.IsReserve = true;

                                if (material.Material.Count < product.ProductAmount * material.MaterialAmount)
                                {
                                    throw new Exception("Не хватает материалов на складах");
                                }
                            }
                        }

                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw e;
                    }
                }
            }
        }
    }
}
