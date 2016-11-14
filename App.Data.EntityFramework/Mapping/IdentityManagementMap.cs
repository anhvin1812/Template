using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Entities.IdentityManagement;

namespace App.Data.EntityFramework.Mapping
{
    public class IdentityManagementMap
    {
        public static void Configure(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PermissionMap());
            //modelBuilder.Configurations.Add(new UserRoleMap());
            //modelBuilder.Configurations.Add(new UserClaimMap());
            // modelBuilder.Configurations.Add(new UserLoginMap());
        }

        private class PermissionMap : EntityTypeConfiguration<Permission>
        {
            public PermissionMap()
            {
                ToTable("Permission");
                // Primary Key
                HasKey(t => t.Id);
                Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                // Properties
                Property(t => t.ClaimType).IsRequired();
                Property(t => t.ClaimValue).IsRequired();
                Property(t => t.Description).IsRequired();

                // Relationships
                HasMany(t => t.Roles).WithMany(p=>p.Permissions).Map(t =>
                {
                    t.ToTable("PermissionRole");
                    t.MapLeftKey("PermissionId");
                    t.MapRightKey("RoleId");
                });
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
