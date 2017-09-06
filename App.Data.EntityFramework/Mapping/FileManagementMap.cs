using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using App.Entities.ProductManagement;
using App.Entities.FileManagement;

namespace App.Data.EntityFramework.Mapping
{
    public class FileManagementMap
    {
        public static void Configure(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GalleryMap());
        }

        private class GalleryMap : EntityTypeConfiguration<Gallery>
        {
            public GalleryMap()
            {
                ToTable("Gallery");
                // Primary Key
                HasKey(t => t.Id);
                Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                // Properties
                Property(t => t.Image).IsRequired();
                Property(t => t.Thumbnail).IsRequired();
            }
        }
    }
}
