using System;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;

namespace Mvc.Web.Core.IoC {
    public class AutoFacConfig {
        //
        public static void ConfigueContainer() {
            var builder = new ContainerBuilder();
            SetupResolveRules(builder);
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void SetupResolveRules(ContainerBuilder builder) {
            //builder.RegisterType<HomeRepository>().As<IHomeRepository>();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();
        }
    }
}