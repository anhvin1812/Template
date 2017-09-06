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
        private RoleManager<Role, int> _roleManager;
        public ApplicationRoleManager(IRoleStore<Role, int> roleStore)
            : base(roleStore)
        {
            //_roleManager = new RoleManager<Role, int>(roleStore);
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            var appRoleManager = new ApplicationRoleManager(new RoleStore<Role, int, UserRole>(context.Get<MinhKhangDbContext>()));

            return appRoleManager;
        }

        public void Update(Role role)
        {
            role.State = ObjectState.Modified;
            _roleManager.Update(role);
        }

        public void Create(Role role)
        {
            role.State = ObjectState.Added;
            _roleManager.Create(role);
        }

        public void Delete(Role role)
        {
            role.State = ObjectState.Deleted;
            _roleManager.Delete(role);
        }

        public override Task<IdentityResult> CreateAsync(Role role)
        {
            role.State = ObjectState.Added;
            var result = base.CreateAsync(role);
            return result;
        }
    }
}
