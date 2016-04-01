using App.Data.EntityFramework;
using App.Entities.IdentityManagement;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace App.Infrastructure.IdentityManagement
{
    public class ApplicationRoleManager : RoleManager<Role,int>
    {
        public ApplicationRoleManager(IRoleStore<Role, int> roleStore)
            : base(roleStore)
        {
        }
        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            var appRoleManager = new ApplicationRoleManager(new RoleStore<Role, int, UserRole>(context.Get<MinhKhangDbContext>()));

            return appRoleManager;
        }
    }
}
