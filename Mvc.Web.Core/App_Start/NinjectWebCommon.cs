[assembly: WebActivator.PreApplicationStartMethod(typeof(Mvc.Web.Core.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(Mvc.Web.Core.App_Start.NinjectWebCommon), "Stop")]

namespace Mvc.Web.Core.App_Start {
    using System;
    using System.Web;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Mvc.Data;
    using Mvc.Data.Repository;
    using Mvc.Data.Repository.Impl;
    using Mvc.Data.UnitOfWork;
    using Mvc.Data.UnitOfWork.Impl;
    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop() {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel() {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel) {
            kernel.Bind<MvcEntities>().ToSelf().InRequestScope();
            kernel.Bind<ICustomerRepository>().To<CustomerRepository>();
            kernel.Bind<IUnitOfWork>().To<EFRepositoryContext>();
        }
    }
}
