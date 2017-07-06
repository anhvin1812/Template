﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using App.Entities.NewsManagement;

namespace App.Data.EntityFramework.Mapping
{
    public class NewsManagementMap
    {
        public static void Configure(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new NewsMap());
            modelBuilder.Configurations.Add(new NewsCategoryMap());
            modelBuilder.Configurations.Add(new TagMap());
        }

        private class NewsMap : EntityTypeConfiguration<News>
        {
            public NewsMap()
            {
                ToTable("News");
                // Primary Key
                HasKey(t => t.Id);
                Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                // Properties
                Property(t => t.Title).IsRequired();
                Property(t => t.GalleryId).IsOptional();
                Property(t => t.Description).IsOptional();
                Property(t => t.Content).IsOptional();
                Property(t => t.IsHot).IsOptional();
                Property(t => t.IsFeatured).IsOptional();
                Property(t => t.IsDisabled).IsOptional();
                Property(t => t.Views).IsRequired();
                Property(t => t.CreatedDate).IsRequired();
                Property(t => t.UpdatedDate).IsOptional();
                Property(t => t.DeletedDate).IsOptional();
                Property(t => t.UpdatedById).IsRequired();

                // Relationships
                HasOptional(t => t.Image).WithMany().HasForeignKey(t => t.GalleryId);
                HasRequired(t => t.Editor).WithMany().HasForeignKey(t => t.UpdatedById);
                HasMany(t => t.Categories).WithMany().Map(x =>
                {
                    x.ToTable("News_NewsCategory");
                    x.MapLeftKey("NewsId");
                    x.MapRightKey("NewsCategory");

                });
            }
        }

        private class NewsCategoryMap : EntityTypeConfiguration<NewsCategory>
        {
            public NewsCategoryMap()
            {
                ToTable("NewsCategory");
                // Primary Key
                HasKey(t => t.Id);
                Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                // Properties
                Property(t => t.Name).IsRequired();
                Property(t => t.Description).IsOptional();
                Property(t => t.ParentId).IsOptional();
                Property(t => t.IsDisabled).IsOptional();

                // Relationships
                HasOptional(t => t.Parent).WithMany().HasForeignKey(t => t.ParentId);
            }
        }

        private class TagMap : EntityTypeConfiguration<Tag>
        {
            public TagMap()
            {
                ToTable("Tag");
                // Primary Key
                HasKey(t => t.Id);
                Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                // Properties
                Property(t => t.Name).IsRequired();
                Property(t => t.IsDisabled).IsOptional();
            }
        }
    }
}
