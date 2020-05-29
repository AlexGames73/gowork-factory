using System.Collections.Generic;

namespace GoWorkFactoryBusinessLogic.Interfaces
{
    public interface ILogic<TBinding, TView>
    {
        void CreateOrUpdate(TBinding model);
        void Remove(TBinding model);
        IEnumerable<TView> Read(TBinding model);
    }
}
