using GoWorkFactoryBusinessLogic.BindingModels;
using GoWorkFactoryBusinessLogic.Interfaces;
using GoWorkFactoryBusinessLogic.ViewModels;
using GoWorkFactoryDataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoWorkFactoryDataBase.Implementations
{
    public class ProductLogic : IProductLogic
    {
        public void CreateOrUpdate(ProductBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                if (context.Products.FirstOrDefault(x => x.Name == model.NameProduct) != null)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }

                Product product = new Product
                {
                    Name = model.NameProduct,
                    Price = model.CostProduct
                };

                context.Products.Add(product);
                context.SaveChanges();

                foreach (var material in model.Materials)
                {
                    context.MaterialProducts.Add(new MaterialProduct
                    {
                        IsReserve = false,
                        MaterialAmount = material.Value.Item2,
                        MaterialId = material.Key,
                        ProductId = product.Id
                    });
                }
                context.SaveChanges();
            }
        }

        public IEnumerable<ProductViewModel> Read(ProductBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                return context.Products
                .Where(rec => model == null || rec.Id == model.Id)
                .ToList()
               .Select(rec => new ProductViewModel
               {
                   Id = rec.Id,
                   NameProduct = rec.Name,
                   CostProduct = rec.Price,
                   Materials = context.MaterialProducts
                .Include(recPC => recPC.Material)
                .Where(recPC => recPC.ProductId == rec.Id)
                .ToDictionary(recPC => recPC.MaterialId, recPC =>
                (recPC.Material?.Name, recPC.MaterialAmount))
               }).ToList();
            }
        }
        
        public void Remove(ProductBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                context.MaterialProducts.RemoveRange(context.MaterialProducts.Where(x => x.ProductId == model.Id));

                Product element = context.Products.FirstOrDefault(x => x.Id == model.Id);

                if (element != null)
                {
                    context.Products.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public void Update(ProductBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                Product product = context.Products.FirstOrDefault(x => x.Id == model.Id);

                if (product == null)
                {
                    throw new Exception("Такого продукта не существует");
                }

                product.Name = model.NameProduct;
                product.Price = model.CostProduct;

                var materialProducts = context.MaterialProducts.Where(x => x.ProductId == model.Id).ToList();

                context.MaterialProducts.RemoveRange(materialProducts.Where(x => !model.Materials.ContainsKey(x.MaterialId)));
                context.SaveChanges();

                foreach (var material in materialProducts)
                {
                    material.MaterialAmount = model.Materials[material.MaterialId].Item2;
                }

                context.SaveChanges();
            }
        }
    }
}
