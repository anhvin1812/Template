using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Entities.ProductManagement;

namespace App.Data.EntityFramework.Mapping
{
    public class IdentityManagementMap
    {
        public static void Configure(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new RoleClaimnMap());
            //modelBuilder.Configurations.Add(new UserRoleMap());
            //modelBuilder.Configurations.Add(new UserClaimMap());
            // modelBuilder.Configurations.Add(new UserLoginMap());
        }

        private class RoleClaimnMap : EntityTypeConfiguration<RoleClaim>
        {
            public RoleClaimnMap()
            {
                ToTable("RoleClaim");
                // Primary Key
                HasKey(t => t.Id);
                Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                // Properties
                Property(t => t.RoleId).IsRequired();
                Property(t => t.ClaimType).IsRequired();
                Property(t => t.ClaimValue).IsRequired();

                // Relationships
                HasRequired(t => t.Role).WithMany(c=>c.RoleClaims).HasForeignKey(r => r.RoleId);
            }
        }

        private class UserRoleMap : EntityTypeConfiguration<Role>
        {
            public UserRoleMap()
            {
                ToTable("UserRole");
                // Primary Key
                HasKey(t => t.Id);
                Property(t => t.Id).IsRequired();

                // Properties
                Property(t => t.Name).IsRequired();

            }
        }

        private class UserClaimMap : EntityTypeConfiguration<UserClaim>
        {
            public UserClaimMap()
            {
                ToTable("UserClaim");
                // Primary Key
                HasKey(t => t.Id);
                Property(t => t.Id).IsRequired();

                // Properties
                Property(t => t.UserId).IsRequired();
                Property(t => t.ClaimType).IsOptional();
                Property(t => t.ClaimValue).IsOptional();
            }
        }

        private class UserLoginMap : EntityTypeConfiguration<UserLogin>
        {
            public UserLoginMap()
            {
                ToTable("UserLogin");
                // Primary Key
                HasKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId });

                // Properties
                Property(t => t.LoginProvider).IsRequired();
                Property(t => t.ProviderKey).IsRequired();
                Property(t => t.UserId).IsRequired();
            }
        }
    }
}
