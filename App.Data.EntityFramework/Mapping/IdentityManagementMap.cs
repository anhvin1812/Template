using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.EntityFramework.Mapping
{
    public class IdentityManagementMap
    {
        public static void Configure(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new UserMap());
            //modelBuilder.Configurations.Add(new UserRoleMap());
            //modelBuilder.Configurations.Add(new UserClaimMap());
            //modelBuilder.Configurations.Add(new UserLoginMap());
        }

        //private class UserMap : EntityTypeConfiguration<User>
        //{
        //    public UserMap()
        //    {
        //        ToTable("User");
        //        // Primary Key
        //        HasKey(t => t.Id);
        //        Property(t => t.Id).IsRequired();

        //        // Properties
        //        Property(t => t.Email).IsOptional();
        //        Property(t => t.EmailConfirmed).IsRequired();
        //        Property(t => t.PasswordHash).IsRequired();
        //        Property(t => t.SecurityStamp).IsOptional();
        //        Property(t => t.PhoneNumber).IsOptional();
        //        Property(t => t.PhoneNumberConfirmed).IsRequired();
        //        Property(t => t.TwoFactorEnabled).IsRequired();
        //        Property(t => t.LockoutEndDateUtc).IsOptional();
        //        Property(t => t.LockoutEnable).IsRequired();
        //        Property(t => t.AccessFailedCount).IsRequired();
        //        Property(t => t.UserName).IsRequired();

        //        // Relationships
        //        HasMany(t => t.Claims).WithRequired().HasForeignKey(t => t.UserId);
        //        HasMany(t => t.Logins).WithRequired().HasForeignKey(t => t.UserId);
        //        HasMany(t => t.Roles).WithMany().Map(t =>
        //        {
        //            t.ToTable("UserUserRole");
        //            t.MapLeftKey("UserId");
        //            t.MapRightKey("RoleId");
        //        });
        //    }
        //}

        //private class UserRoleMap : EntityTypeConfiguration<UserRole>
        //{
        //    public UserRoleMap()
        //    {
        //        ToTable("UserRole");
        //        // Primary Key
        //        HasKey(t => t.Id);
        //        Property(t => t.Id).IsRequired();

        //        // Properties
        //        Property(t => t.Name).IsRequired();
               
        //    }
        //}

        //private class UserClaimMap : EntityTypeConfiguration<UserClaim>
        //{
        //    public UserClaimMap()
        //    {
        //        ToTable("UserClaim");
        //        // Primary Key
        //        HasKey(t => t.Id);
        //        Property(t => t.Id).IsRequired();

        //        // Properties
        //        Property(t => t.UserId).IsRequired();
        //        Property(t => t.ClaimType).IsOptional();
        //        Property(t => t.ClaimValue).IsOptional();
        //    }
        //}

        //private class UserLoginMap : EntityTypeConfiguration<UserLogin>
        //{
        //    public UserLoginMap()
        //    {
        //        ToTable("UserLogin");
        //        // Primary Key
        //        HasKey(t => new {t.LoginProvider, t.ProviderKey, t.UserId});

        //        // Properties
        //        Property(t => t.LoginProvider).IsRequired();
        //        Property(t => t.ProviderKey).IsRequired();
        //        Property(t => t.UserId).IsRequired();
        //    }
        //}
    }
}
