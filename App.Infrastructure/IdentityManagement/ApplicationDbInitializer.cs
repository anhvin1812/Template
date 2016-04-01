using System.Data.Entity;
using System.Linq;
using App.Data.EntityFramework;
using App.Entities.IdentityManagement;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace App.Infrastructure.IdentityManagement
{
    public class ApplicationDbInitializer : DropCreateDatabaseAlways<MinhKhangDbContext>
    {
    
        protected override void Seed(MinhKhangDbContext context)
        {
            InitializeIdentityForEF(context);
            base.Seed(context);
        }


        public static void InitializeIdentityForEF(MinhKhangDbContext context)
        {
            //context = new MinhKhangDbContext("MinhKhang");
            var userStore = new UserStore<User, Role, int, UserLogin, UserRole, UserClaim>(context);
            var roleStore = new RoleStore<Role, int, UserRole>(context);

            var userManager = new ApplicationUserManager(userStore);
            var roleManager = new ApplicationRoleManager(roleStore);

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
            //context.Users.Add(user);
            userManager.Create(user, "Admin@123");

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new Role { Name = "Admin" });
                roleManager.Create(new Role { Name = "User" });
            }

            var adminUser = userManager.FindByName("Admin");

            userManager.AddToRoles(adminUser.Id, new string[] { "Admin" });

              //You can use the DbSet<T>.AddOrUpdate() helper extension method 
              //to avoid creating duplicate seed data. E.g.
            
              //  context.People.AddOrUpdate(
              //    p => p.FullName,
              //    new Person { FullName = "Andrew Peters" },
              //    new Person { FullName = "Brice Lambson" },
              //    new Person { FullName = "Rowan Miller" }
              //  );
            

            context.SaveChanges();
        }


    }
}