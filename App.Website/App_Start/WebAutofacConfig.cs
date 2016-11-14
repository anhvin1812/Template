using System.Web.Mvc;
using App.Services;
using App.Website.App_Start;
using Autofac;
using Autofac.Integration.Mvc;

namespace App.Website
{
    public class WebAutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            // Register dependencies in controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Register dependencies in filter attributes
            builder.RegisterFilterProvider();

            // Register dependencies in custom views
            builder.RegisterSource(new ViewRegistrationSource());

            // Register our Data dependencies
            builder.RegisterModule(new AppAutofacModule());

            var container = builder.Build();

            // Set MVC DI resolver to use our Autofac container
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}