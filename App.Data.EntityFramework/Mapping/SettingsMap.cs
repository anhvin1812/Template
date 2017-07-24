using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using App.Entities.NewsManagement;
using App.Entities.Settings;

namespace App.Data.EntityFramework.Mapping
{
    public class SettingsMap
    {
        public static void Configure(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new SettingMap());
            modelBuilder.Configurations.Add(new HomepageLayoutMap());
        }

        private class SettingMap : EntityTypeConfiguration<Setting>
        {
            public SettingMap()
            {
                ToTable("Setting");
                // Primary Key
                HasKey(t => t.Id);
                Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                // Properties
                Property(t => t.Menu).IsOptional();
                Property(t => t.Address).IsOptional();
                Property(t => t.PhoneNumber).IsOptional();
                Property(t => t.Skype).IsOptional();
                Property(t => t.Email).IsOptional();
                Property(t => t.Facebook).IsOptional();
                Property(t => t.Website).IsOptional();
                Property(t => t.Logo).IsOptional();
            }
        }

        private class HomepageLayoutMap : EntityTypeConfiguration<HomepageLayout>
        {
            public HomepageLayoutMap()
            {
                ToTable("HomepageLayout");
                // Primary Key
                HasKey(t => t.Id);
                Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                // Properties
                Property(t => t.CategoryId).IsOptional();
                Property(t => t.MediaTypeId).IsOptional();
                Property(t => t.LayoutTypeId).IsOptional();
                Property(t => t.SortOrder).IsOptional();
            }
        }
    }
}
