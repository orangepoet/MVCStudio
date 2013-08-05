using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Ninject.Syntax;

namespace Mvc.Web.Core.IoC {
    /// <summary>
    /// 使用时, 需要在Global文件的Application_Start()中设置
    ///  DependencyResolver.SetResolver(new NinjectResolver());
    /// </summary>
    public class NinjectDependencyResolver : IDependencyResolver {
        private IKernel kernel;

        public NinjectDependencyResolver() {
            kernel = new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType) {
            //return kernel.GetService(serviceType);
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return kernel.GetAll(serviceType);
        }

        public IBindingToSyntax<T> Bind<T>() {
            return kernel.Bind<T>();
        }

        public IKernel Kernel {
            get { return kernel; }
        }

        private void AddBindings() {
            //Bind<IViewEngine>().To<OrangeViewEngine>();

            //Binding
            //Bind<ICustomerService>().To<CustomerService>();
            //Bind<IGroupService>().To<GroupService>();
            //Bind<IChargeService>().To<ChargeService>();
            //Bind<IModuleService>().To<ModuleService>();
            //Bind<IJobService>().To<JobService>();
            //Bind<IVenueStaffService>().To<VenueStaffService>();
            //Bind<IStoreService>().To<StoreService>();
            //Bind<IRackService>().To<RackService>();
            //Bind<IProductService>().To<ProductService>();
            //Bind<IProductBrandService>().To<ProductBrandService>();
            //Bind<IContractService>().To<ContractService>();
        }
    }
}