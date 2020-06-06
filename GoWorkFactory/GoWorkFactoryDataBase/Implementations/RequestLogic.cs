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
        public RequestViewModel CreateOrUpdate(RequestBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                Request element;
                if (model.Id.HasValue)
                {
                    element = context.Requests.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element != null)
                    {
                        throw new Exception("Заявку нельзя изменить");
                    }
                }
                else
                {
                    element = new Request
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
                return GetViewModel(element);
            }
        }

        public IEnumerable<RequestViewModel> Read(RequestBindingModel model)
        {
            using (var context = new GoWorkFactoryDataBaseContext())
            {
                return context.Requests
                .Include(rec => rec.MaterialRequests)
                .ThenInclude(rec => rec.Material)
                .Where(rec => model == null || rec.Id == model.Id
                || (model.DateFrom.HasValue && model.DateTo.HasValue && rec.Date >= model.DateFrom && rec.Date <= model.DateTo))
                .Select(GetViewModel)
                .ToList();
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

        private RequestViewModel GetViewModel(Request request)
        {
            return new RequestViewModel
            {
                Id = request.Id,
                UserId = request.UserId,
                Date = request.Date,
                Materials = request.MaterialRequests.ToDictionary(recPC => recPC.MaterialId, recPC => (recPC.Material.Name, recPC.Count, recPC.Price))
            };
        }
    }
}
