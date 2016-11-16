using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Repositories;
using App.Data.EntityFramework;
using App.Entities.IdentityManagement;
using App.Infrastructure.IdentityManagement;
using Autofac;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace App.Infrastructure
{
    public class InfrastructureAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationUserManager>().As<ApplicationUserManager>().InstancePerDependency();
            builder.RegisterType<ApplicationRoleManager>().As<ApplicationRoleManager>().InstancePerDependency();

            builder.Register<IUserStore<User, int>>(c => new UserStore<User, Role, int, UserLogin, UserRole, UserClaim>(new MinhKhangDbContext())).InstancePerDependency();
            builder.Register<IRoleStore<Role, int>>(c => new RoleStore<Role, int, UserRole>(new MinhKhangDbContext())).InstancePerDependency();
        
        }
    }
}
