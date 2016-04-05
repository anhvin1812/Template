using System.Threading.Tasks;
using App.Data.EntityFramework;
using App.Entities;
using App.Entities.IdentityManagement;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace App.Infrastructure.IdentityManagement
{
    public class ApplicationRoleManager : RoleManager<Role,int>
    {
        private RoleManager<Role, int> _oleManager;
        public ApplicationRoleManager(IRoleStore<Role, int> roleStore)
            : base(roleStore)
        {
            _oleManager = new RoleManager<Role, int>(roleStore);
        }
        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            var appRoleManager = new ApplicationRoleManager(new RoleStore<Role, int, UserRole>(context.Get<MinhKhangDbContext>()));

            return appRoleManager;
        }

        public void Update(Role role)
        {
            //_oleManager.Update(role);
            role.State = ObjectState.Modified;
        }

        public override Task<IdentityResult> CreateAsync(Role role)
        {
            role.State = ObjectState.Added;
            var result = base.CreateAsync(role);
            return result;
        }
    }
}
