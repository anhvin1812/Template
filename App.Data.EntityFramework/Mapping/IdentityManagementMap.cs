using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using App.Entities.IdentityManagement;

namespace App.Data.EntityFramework.Mapping
{
    public class IdentityManagementMap
    {
        public static void Configure(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new UserMap());
            //modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new RoleClaimMap());
            //modelBuilder.Configurations.Add(new UserRoleMap());
            //modelBuilder.Configurations.Add(new UserClaimMap());
            //modelBuilder.Configurations.Add(new UserLoginMap());
        }

        private class UserMap : EntityTypeConfiguration<User>
        {
            public UserMap()
            {
                ToTable("User");
                // Primary Key
                HasKey(t => t.Id);
                Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                // Properties
                Property(t => t.Id).IsRequired();
                Property(t => t.Firstname).IsOptional();
                Property(t => t.Lastname).IsOptional();
                Property(t => t.Address).IsOptional();
                Property(t => t.DateOfBirth).IsOptional();
                Property(t => t.Gender).IsOptional();
                Property(t => t.UserName).IsRequired();
                Property(t => t.Email).IsRequired();
                Property(t => t.EmailConfirmed).IsRequired();
                Property(t => t.PasswordHash).IsOptional();
                Property(t => t.SecurityStamp).IsOptional();
                Property(t => t.PhoneNumber).IsOptional();
                Property(t => t.PhoneNumberConfirmed).IsRequired();
                Property(t => t.TwoFactorEnabled).IsRequired();
                Property(t => t.LockoutEndDateUtc).IsOptional();
                Property(t => t.LockoutEnabled).IsRequired();
                Property(t => t.AccessFailedCount).IsRequired();
                Property(t => t.GalleryId).IsOptional();

                // Relationships
                HasOptional(t => t.ProfilePicture).WithMany().HasForeignKey(r => r.GalleryId);
                HasMany(t => t.UserRoles).WithMany(t => t.RoleUsers).Map(x =>
                {
                    x.ToTable("UserRole");
                    x.MapLeftKey("UserId");
                    x.MapRightKey("RoleId");
                });
            }
        }

        private class RoleClaimMap : EntityTypeConfiguration<RoleClaim>
        {
            public RoleClaimMap()
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

        private class RoleMap : EntityTypeConfiguration<Role>
        {
            public RoleMap()
            {
                ToTable("Role");
                // Primary Key
                HasKey(t => t.Id);
                Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                // Properties
                Property(t => t.Name).IsRequired();
                Property(t => t.Description).IsOptional();
            }
        }

        private class UserRoleMap : EntityTypeConfiguration<UserRole>
        {
            public UserRoleMap()
            {
                ToTable("UserRole");
                // Primary Key
                HasKey(t => new { t.UserId , t.RoleId});

                // Properties
                Property(t => t.UserId).IsRequired();
                Property(t => t.RoleId).IsRequired();
            }
        }

        private class UserClaimMap : EntityTypeConfiguration<UserClaim>
        {
            public UserClaimMap()
            {
                ToTable("UserClaim");
                // Primary Key
                HasKey(t => t.Id);
                Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

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
