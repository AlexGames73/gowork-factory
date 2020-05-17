using System.Collections.Generic;

namespace GoWorkFactoryBusinessLogic.Interfaces
{
    public interface ILogic<TBinding, TView>
    {
        void Create(TBinding model);
        void Update(TBinding model);
        void Remove(TBinding model);
        IEnumerable<TView> Read(TBinding model);
    }
}
