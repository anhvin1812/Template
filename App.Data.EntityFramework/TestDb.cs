using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using App.Core.Repositories;
using App.Entities;
using App.Entities.ProductManagement;
using Microsoft.AspNet.Identity.EntityFramework;

namespace App.Data.EntityFramework
{
    public  class TestDb : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>
    {

        public  TestDb(string nameOrConnectionString="MinhKhang")
            : base(nameOrConnectionString)
        {
            
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

#if DEBUG
            Database.Log = s => Debug.Write(s);
#endif

            // Add entities for Identity
            modelBuilder.Entity<UserRole>().ToTable("UserRole");
            modelBuilder.Entity<UserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<User>().ToTable("User");

            // Mapping
           // IdentityManagementMap.Configure(modelBuilder);

        }

    }
}
