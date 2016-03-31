using System.Data.Entity;
using System.Linq;
using App.Core.Identity;
using App.Data.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace App.Infrastructure.IdentityManagement
{
    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<MinhKhangDbContext>
    {
    
        protected override void Seed(MinhKhangDbContext context)
        {
            InitializeIdentityForEF(context);
            base.Seed(context);
        }


        public static void InitializeIdentityForEF(MinhKhangDbContext context)
        {
            var store = new UserStore<User, Role, int, UserLogin, UserRole, UserClaim>(context);


            var userManager = new UserManager<User, int>(store);
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            //var userManager = HttpContext
            //.Current.GetOwinContext()
            //.GetUserManager<ApplicationUserManager>();

            //var roleManager = HttpContext.Current
            //    .GetOwinContext()
            //    .Get<ApplicationRoleManager>();

            var user = new User()
            {
                UserName = "Admin",
                Email = "leanhvin@gmail.com",
                EmailConfirmed = true,
            };

            userManager.Create(user, "Admin@123");

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = userManager.FindByName("Admin");

            userManager.AddToRoles(adminUser.Id, new string[] { "Admin" });

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }


    }
}