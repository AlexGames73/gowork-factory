using GoWorkFactoryBusinessLogic.BindingModels;
using GoWorkFactoryBusinessLogic.ViewModels;

namespace GoWorkFactoryBusinessLogic.Interfaces
{
    public interface IOrderLogic : ILogic<OrderBindingModel, OrderViewModel>
    {
        void AddProduct(ChangeProductOrderBindingModel model);
        void RemoveProduct(ChangeProductOrderBindingModel model);

        void ReservationOrder(ReservationBindingModel model);
    }
}
