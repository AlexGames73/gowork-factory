﻿using GoWorkFactoryBusinessLogic.BindingModels;
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
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Product element = context.Products.FirstOrDefault(rec =>
                        rec.Name == model.NameProduct && rec.Id != model.Id);
                        if (element != null)
                        {
                            throw new Exception("Уже есть изделие с таким названием");
                        }
                        if (model.Id.HasValue)
                        {
                            element = context.Products.FirstOrDefault(rec => rec.Id ==
                            model.Id);
                            if (element == null)
                            {
                                throw new Exception("Элемент не найден");
                            }
                        }
                        else
                        {
                            element = new Product();
                            context.Products.Add(element);
                        }
                        element.Name = model.NameProduct;
                        element.Price = model.CostProduct;
                        context.SaveChanges();
                        if (model.Id.HasValue)
                        {
                            var materialProducts = context.MaterialProducts.Where(rec
                                => rec.ProductId == model.Id.Value).ToList();

                            context.MaterialProducts.RemoveRange(materialProducts.Where(rec =>
                            !model.Materials.ContainsKey(rec.MaterialId)).ToList());
                            context.SaveChanges();

                            foreach (var materialProduct in materialProducts)
                            {
                                materialProduct.MaterialAmount =
                                model.Materials[materialProduct.MaterialId].Item2;

                                model.Materials.Remove(materialProduct.MaterialId);
                            }
                            context.SaveChanges();
                        }
                        // добавили новые
                        foreach (var pc in model.Materials)
                        {
                            context.MaterialProducts.Add(new MaterialProduct
                            {
                                ProductId = element.Id,
                                MaterialId = pc.Key,
                                MaterialAmount = pc.Value.Item2
                            });
                            context.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
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
    }
}