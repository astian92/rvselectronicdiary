using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using RED.Filters;
using RED.Models.DataContext.Abstract;
using RED.Models.DataContext.Concrete;

namespace RED.App_Start
{
    public class ContainerConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly).InstancePerRequest();
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModule<AutofacWebTypesModule>();

            builder.RegisterFilterProvider();
            builder.RegisterType<RoleFilter>().PropertiesAutowired();
            builder.RegisterType<UserFilter>().PropertiesAutowired();
            builder.RegisterType<RoleAuthorizationFilter>().PropertiesAutowired();

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