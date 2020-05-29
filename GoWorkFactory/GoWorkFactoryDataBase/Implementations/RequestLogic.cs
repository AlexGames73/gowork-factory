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
    public class RequestLogic : IRequestLogic
    {
        public void CreateOrUpdate(RequestBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                if (model.Id.HasValue)
                {
                    Request element = context.Requests.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element != null)
                    {
                        throw new Exception("Заявку нельзя изменить");
                    }
                }
                else
                {
                    Request element = new Request
                    {
                        UserId = model.UserId,
                        Date = model.Date
                    };

                    context.Requests.Add(element);
                    context.SaveChanges();

                    foreach (var material in model.Materials)
                    {
                        context.MaterialRequests.Add(new MaterialRequest
                        {
                            MaterialId = material.Key,
                            Count = material.Value.Item1,
                            Price = material.Value.Item2,
                            RequestId = element.Id
                        });
                    }
                }
                context.SaveChanges();
            }
        }

        public IEnumerable<RequestViewModel> Read(RequestBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                return context.Requests
                .Where(rec => model == null || rec.Id == model.Id)
                .ToList()
               .Select(rec => new RequestViewModel
               {
                   Id = rec.Id,
                   UserId = rec.UserId,
                   Date = rec.Date,
                   Materials = context.MaterialRequests
                .Include(recPC => recPC.Material)
                .Where(recPC => recPC.RequestId == rec.Id)
                .ToDictionary(recPC => recPC.MaterialId, recPC =>
                (recPC.Count, recPC.Price))
               }).ToList();
            }
        }

        public void Remove(RequestBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                context.MaterialRequests.RemoveRange(context.MaterialRequests.Where(x => x.RequestId == model.Id));

                Request element = context.Requests.FirstOrDefault(x => x.Id == model.Id);

                if (element != null)
                {
                    context.Requests.Remove(element);
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
