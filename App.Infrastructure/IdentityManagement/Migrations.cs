using App.Data.EntityFramework;
using App.Entities.IdentityManagement;
using Microsoft.AspNet.Identity.EntityFramework;

namespace App.Infrastructure.IdentityManagement
{
    public class Migrations 
    {
        public static void Initialize()
        {
            //Database.SetInitializer(new ApplicationDbInitializer());
            //db.Roles.Add(new Role("ADmin"));
            //db.SaveChanges();
           
            //db.Database.Initialize(true);

            MinhKhangDbContext db = new MinhKhangDbContext();

            var userStore = new UserStore<User, Role, int, UserLogin, UserRole, UserClaim>(db);
            var roleStore = new RoleStore<Role, int, UserRole>(db);

            var userManager = new ApplicationUserManager(userStore);
            var roleManager = new ApplicationRoleManager(roleStore);
            roleManager.CreateAsync(new Role("Admin"));

            db.SaveChanges();

        }
    }
}
