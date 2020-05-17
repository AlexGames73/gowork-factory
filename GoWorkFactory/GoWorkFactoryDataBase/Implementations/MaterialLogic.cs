using GoWorkFactoryBusinessLogic.BindingModels;
using GoWorkFactoryBusinessLogic.Interfaces;
using GoWorkFactoryBusinessLogic.ViewModels;
using GoWorkFactoryDataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoWorkFactoryDataBase.Implementations
{
    public class MaterialLogic : IMaterialLogic
    {
        public void Create(MaterialBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                if (context.Materials.FirstOrDefault(x => x.Name == model.NameMaterial) != null)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }

                Material material = new Material
                {
                    Name = model.NameMaterial,
                    Count = model.CountMaterial
                };
                context.Materials.Add(material);
                context.SaveChanges();
            }
        }

        public IEnumerable<MaterialViewModel> Read(MaterialBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                return context.Materials
                .Where(rec => model == null || rec.Id == model.Id)
                .Select(rec => new MaterialViewModel
                {
                    Id = rec.Id,
                    NameMaterial = rec.Name,
                    CountMaterial = rec.Count
                })
                .ToList();
            }
        }

        public void Remove(MaterialBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                Material material = context.Materials.FirstOrDefault(x => x.Id == model.Id);
                if (material != null)
                {
                    context.Materials.Remove(material);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public void Update(MaterialBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                Material material = context.Materials.FirstOrDefault(x => x.Id == model.Id);

                if (material == null)
                {
                    throw new Exception("Такого продукта не существует");
                }

                material.Count = model.CountMaterial;
                material.Name = model.NameMaterial;

                context.SaveChanges();
            }
        }
    }
}
