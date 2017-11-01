using System.ComponentModel.DataAnnotations.Schema;
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
            modelBuilder.Configurations.Add(new NewsStatusMap());
            modelBuilder.Configurations.Add(new CrawlSourceMap());
            modelBuilder.Configurations.Add(new CrawlSourcePageMap());
            modelBuilder.Configurations.Add(new CrawlSourcePageDetailMap());
            modelBuilder.Configurations.Add(new CrawlArticleSectionMap());
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
                Property(t => t.StatusId).IsRequired();
                Property(t => t.MediaTypeId).IsRequired();
                Property(t => t.Views).IsRequired();
                Property(t => t.CreatedDate).IsRequired();
                Property(t => t.UpdatedDate).IsOptional();
                Property(t => t.DeletedDate).IsOptional();
                Property(t => t.UpdatedById).IsRequired();

                // Relationships
                HasOptional(t => t.Image).WithMany().HasForeignKey(t => t.GalleryId);
                HasRequired(t => t.Editor).WithMany().HasForeignKey(t => t.UpdatedById);
                HasRequired(t => t.Status).WithMany().HasForeignKey(t => t.StatusId);
                HasMany(t => t.Categories).WithMany( t=>t.Newses).Map(x =>
                {
                    x.ToTable("News_NewsCategory");
                    x.MapLeftKey("NewsId");
                    x.MapRightKey("NewsCategoryId");

                });
                HasMany(t => t.Tags).WithMany(t => t.Newses).Map(x =>
                {
                    x.ToTable("News_Tag");
                    x.MapLeftKey("NewsId");
                    x.MapRightKey("TagId");

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
                HasMany(t => t.Newses).WithMany();
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

                // Relationships
                HasMany(t => t.Newses).WithMany();
            }
        }

        private class NewsStatusMap : EntityTypeConfiguration<NewsStatus>
        {
            public NewsStatusMap()
            {
                ToTable("NewsStatus");
                // Primary Key
                HasKey(t => t.Id);
                Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                // Properties
                Property(t => t.Status).IsRequired();
            }
        }

        #region Crawl
        private class CrawlSourceMap : EntityTypeConfiguration<CrawlSource>
        {
            public CrawlSourceMap()
            {
                ToTable("CrawlSource");
                // Primary Key
                HasKey(t => t.Id);
                Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                // Properties
                Property(t => t.Name).IsRequired();
                Property(t => t.Url).IsOptional();
                Property(t => t.IsDisabled).IsOptional();
            }
        }

        private class CrawlSourcePageMap : EntityTypeConfiguration<CrawlSourcePage>
        {
            public CrawlSourcePageMap()
            {
                ToTable("CrawlSourcePage");
                // Primary Key
                HasKey(t => t.Id);
                Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                // Properties
                Property(t => t.CrawlSourceId).IsRequired();
                Property(t => t.Name).IsRequired();
                Property(t => t.Url).IsRequired();
                Property(t => t.IsRelativeUrl).IsRequired();
                Property(t => t.BaseUrl).IsOptional();

                // Relationship
                HasRequired(t => t.CrawlSource).WithMany().HasForeignKey(t => t.CrawlSourceId);

            }
        }

        private class CrawlSourcePageDetailMap : EntityTypeConfiguration<CrawlSourcePageDetail>
        {
            public CrawlSourcePageDetailMap()
            {
                ToTable("CrawlSourcePageDetail");
                // Primary Key
                HasKey(t => t.Id);
                Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                // Properties
                Property(t => t.CrawlSourceId).IsRequired();
                Property(t => t.TitleSelector).IsRequired();
                Property(t => t.DescriptionSelector).IsRequired();
                Property(t => t.ContentSelector).IsRequired();
                Property(t => t.RemoveFromContentSelector).IsOptional();
                Property(t => t.DateSelector).IsOptional();
                Property(t => t.DateFormat).IsOptional();
                Property(t => t.EditorSelector).IsOptional();
                Property(t => t.TagSelector).IsOptional();
                Property(t => t.CategorySelector).IsOptional();
                Property(t => t.VideoSelector).IsOptional();
                Property(t => t.VideoSourceSelector).IsOptional();
                Property(t => t.BaseUrl).IsOptional();
            }
        }

        private class CrawlArticleSectionMap : EntityTypeConfiguration<CrawlArticleSection>
        {
            public CrawlArticleSectionMap()
            {
                ToTable("CrawlArticleSection");
                // Primary Key
                HasKey(t => t.Id);
                Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                // Properties
                Property(t => t.CrawlSourcePageId).IsRequired();
                Property(t => t.Name).IsRequired();
                Property(t => t.Selector).IsRequired();
                Property(t => t.TitleSelector).IsRequired();
                Property(t => t.LinkSelector).IsRequired();
                Property(t => t.DescriptionSelector).IsRequired();
                Property(t => t.FeaturedImageSelector).IsRequired();
                Property(t => t.FeaturedImageAttribute).IsOptional();
                Property(t => t.FeatureImageSizeIdentity).IsRequired();
                Property(t => t.LargeFeatureImageSizeIdentity).IsRequired();
                Property(t => t.IsRelativeUrl).IsRequired();
                Property(t => t.BaseUrl).IsOptional();

                // Relationship
                HasRequired(t => t.CrawlSourcePage).WithMany().HasForeignKey(t => t.CrawlSourcePageId);

            }
        }
        #endregion
    }
}
