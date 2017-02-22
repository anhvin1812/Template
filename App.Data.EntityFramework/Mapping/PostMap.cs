using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using App.Entities.ProductManagement;

namespace App.Data.EntityFramework.Mapping
{
    public class PostMap
    {
        public static void Configure(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CattegoryMap());
            //modelBuilder.Configurations.Add(new UserRoleMap());
            //modelBuilder.Configurations.Add(new UserClaimMap());
            // modelBuilder.Configurations.Add(new UserLoginMap());
        }

        private class CattegoryMap : EntityTypeConfiguration<Category>
        {
            public CattegoryMap()
            {
                ToTable("Category");
                // Primary Key
                HasKey(t => t.Id);
                Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                // Properties
                Property(t => t.Name).IsRequired();
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
