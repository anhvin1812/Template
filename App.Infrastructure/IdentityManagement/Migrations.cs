using System.Data.Entity;
using System.Linq;
using App.Core.Repositories;
using App.Data.EntityFramework;
using App.Entities;
using App.Entities.IdentityManagement;
using App.Entities.Post;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace App.Infrastructure.IdentityManagement
{
    public class Migrations
    {
        public static void Initialize()
        {
           IMinhKhangDatabaseContext db = new MinhKhangDatabaseContext("MinhKhang");

           var userStore = new UserStore<User, Role, int, UserLogin, UserRole, UserClaim>(db.MinhKhangDbContext);
           var roleStore = new RoleStore<Role, int, UserRole>(db.MinhKhangDbContext);
           var roleManager = new ApplicationRoleManager(roleStore);
           var userManager = new ApplicationUserManager(userStore);

           var user = new User()
           {
               UserName = "Admin",
               Email = "leanhvin@gmail.com",
               EmailConfirmed = true,
           };

           if (!userManager.Users.Any())
           {
               userManager.CreateAsync(user, "Admin@123");
               db.SaveChanges();
           }

           if (!roleManager.Roles.Any())
           {
               roleManager.CreateAsync(new Role { Name = "Admin" });
               roleManager.CreateAsync(new Role { Name = "User" });
                db.SaveChanges();
           }

           var adminUser = userManager.FindByName("leanhvin@gmail.com");
           var adminRole = roleManager.FindByName("Admin");
           if (!adminUser.Roles.Any(x => x.RoleId == adminRole.Id))
           {
               var userRole = new UserRole { RoleId = 1, UserId = adminUser.Id, State = ObjectState.Added };
               adminUser.Roles.Add(userRole);
           }

           db.SaveChanges();


            //  MinhKhangDbContext db = new MinhKhangDbContext();
            //db.Database.Initialize(true);
            //db.Roles.Add(new Role("ADmin"));
            //db.SaveChanges();

            //db.Database.Initialize(true);

            //            MinhKhangDbContext db = new MinhKhangDbContext("MinhKhang");

            //IMinhKhangDatabaseContext database = new MinhKhangDatabaseContext("MinhKhang");

            //            //database.Insert(new Category {Name = "Category 1"});
            //            //database.SaveChanges();

            //            var cate = database.Get<Category>().ToList();

            //var db = new TestDb();
            //db.MinhKhangDbContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            ////var userStore = new UserStore<User, Role, int, UserLogin, UserRole, UserClaim>(db);
            //var roleStore = new RoleStore<Role, int, UserRole>(db.MinhKhangDbContext);
            //var roleManager = new ApplicationRoleManager(roleStore);
            //var role = roleManager.FindById(2);
            //role.Name = "Updated 2";


            //roleManager.Update(role);
            //db.SaveChanges();


            //var userManager = new ApplicationUserManager(userStore);
            ////roleManager.CreateAsync(new Role("Admin"));
            ////var role = new Role("Admin");
            ////db.Roles.Add(role);
            ////role.State = ObjectState.Added;
            //var role = roleManager.FindById(1);
            ////var role2 = db.Roles.Find(1);

            //db.SaveChanges();
            // var db = new TestDb();
            // var manager = new RoleManager<Role, int>(new RoleStore<Role, int, UserRole>(db));
            // db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            //// db.Roles.Add(new Role {Name = "Admin 1"});
            // manager.Create(new Role {Name = "Admin 1"});
            // db.SaveChanges();

        }
    }
}
