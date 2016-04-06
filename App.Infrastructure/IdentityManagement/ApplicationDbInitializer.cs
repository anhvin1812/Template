using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using App.Data.EntityFramework;
using App.Entities;
using App.Entities.IdentityManagement;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace App.Infrastructure.IdentityManagement
{
    public class ApplicationDbInitializer : DropCreateDatabaseAlways<MinhKhangDbContext>
    {

        protected override void Seed(MinhKhangDbContext context)
        {
            InitializeIdentity(context);
            base.Seed(context);
        }


        public static void InitializeIdentity(MinhKhangDbContext context)
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
            userManager.CreateAsync(user, "Admin@123");
            context.SaveChanges();
            if (!roleManager.Roles.Any())
            {
                roleManager.CreateAsync(new Role {Name = "Admin"});
                roleManager.CreateAsync(new Role {Name = "User"});
            }
            context.SaveChanges();


            var adminUser = userManager.FindByName("Admin");
            var adminRole = roleManager.FindByName("Admin");
            if (!adminUser.Roles.Any(x => x.RoleId == adminRole.Id))
            {
                var userRole = new UserRole {RoleId = 1, UserId = adminUser.Id, State = ObjectState.Added};
                adminUser.Roles.Add(userRole);
            }
            context.SaveChanges();
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

    internal sealed class Configuration : DbMigrationsConfiguration<MinhKhangDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MinhKhangDbContext context)
        {
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
            userManager.CreateAsync(user, "Admin@123");
            context.SaveChanges();
            if (!roleManager.Roles.Any())
            {
                roleManager.CreateAsync(new Role {Name = "Admin"});
                roleManager.CreateAsync(new Role {Name = "User"});
            }
            context.SaveChanges();


            var adminUser = userManager.FindByName("Admin");
            var adminRole = roleManager.FindByName("Admin");
            if (!adminUser.Roles.Any(x => x.RoleId == adminRole.Id))
            {
                var userRole = new UserRole {RoleId = 1, UserId = adminUser.Id, State = ObjectState.Added};
                adminUser.Roles.Add(userRole);
            }
            context.SaveChanges();

            
        }
    }

}