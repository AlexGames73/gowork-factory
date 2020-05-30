using GoWorkFactoryBusinessLogic.Interfaces;
using GoWorkFactoryDataBase.Implementations;
using GoWorkFactoryViewAdministrator.Forms;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace GoWorkFactoryViewAdministrator
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<AuthorizationForm>());
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IMaterialLogic, MaterialLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderLogic, OrderLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IProductLogic, ProductLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IRequestLogic, RequestLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IUserLogic, UserLogic>(new
           HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
