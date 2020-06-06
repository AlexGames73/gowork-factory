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

        public MaterialViewModel CreateOrUpdate(MaterialBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                Material element = new Material();
                if (model.Id.HasValue)
                {
                    element = context.Materials.FirstOrDefault(rec => rec.Id == model.Id);

                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    else
                    {
                        element.Name = model.Name;
                        element.Count = model.Count;
                    }
                }
                else
                {
                    element = new Material
                    {
                        Name = model.Name,
                        Count = model.Count
                    };
                    context.Materials.Add(element);
                }

                context.SaveChanges();
                return GetViewModel(element);
            }
        }

        public IEnumerable<MaterialViewModel> Read(MaterialBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                return context.Materials
                .Where(rec => model == null || rec.Id == model.Id)
                .Select(GetViewModel)
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

        private MaterialViewModel GetViewModel(Material material)
        {
            return new MaterialViewModel
            {
                Id = material.Id,
                CountMaterial = material.Count,
                NameMaterial = material.Name
            };
        }
    }
}
