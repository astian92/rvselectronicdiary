using System.Web.Mvc;
using System.Reflection;
using Autofac;
using Autofac.Integration.Mvc;
using RED.Models.DataContext.Concrete;
using RED.Models.DataContext.Abstract;

namespace RED.App_Start
{
    public class DependencyInjection
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly).InstancePerRequest();
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModule<AutofacWebTypesModule>();
            
            var dataAccess = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(dataAccess)
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterType<RvsContextFactory>().As<IRvsContextFactory>().InstancePerRequest();

            builder.RegisterModelBinderProvider();

            // Set Autofac as the dependency resolver
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}