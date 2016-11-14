using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.Core.Repositories;
using App.Infrastructure;
using App.Services;
using Autofac;

namespace App.Website.App_Start
{
    public class AppAutofacModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerDependency();

            builder.RegisterModule(new ServicesAutoFacModule());
            builder.RegisterModule(new InfrastructureAutofacModule());
        }
    }
}