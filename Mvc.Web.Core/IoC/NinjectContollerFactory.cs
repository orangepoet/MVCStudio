using System;
using System.Web.Mvc;
using Ninject;

namespace Mvc.Web.Core.IoC {
    /// <summary>
    /// 使用时, 需要在Global文件的Application_Start()中设置:
    /// ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
    /// </summary>
    public class NinjectContollerFactory : DefaultControllerFactory {
        private IKernel kernel;
        public NinjectContollerFactory() {
            kernel = new StandardKernel();
            AddBindings();
        }

        private void AddBindings() {
            //kernel.Bind<ICustomerService>().To<CustomerService>();

            //Mock<IChargeService> mock = new Mock<IChargeService>();
            //mock.Setup(c => c.GetList(It.IsAny<Nullable<bool>>()))
            //    .Returns(new List<Charge> {
            //        new Charge{ ChargeId=1, Amount=2, Date=DateTime.Now, Price=3, Type="水"},
            //        new Charge{ ChargeId=1, Amount=2, Date=DateTime.Now, Price=3, Type="电"}
            //    });
            //kernel.Bind<IChargeService>().ToConstant(mock.Object);
            //kernel.Bind<IChargeService>().To<ChargeService>();
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType) {
            //return base.GetControllerInstance(requestContext, controllerType);
            return controllerType == null ?
                null : (IController)kernel.Get(controllerType);
        }
    }
}