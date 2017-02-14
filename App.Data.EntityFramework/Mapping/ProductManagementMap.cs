﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Entities.IdentityManagement;
using App.Entities.ProductManagement;

namespace App.Data.EntityFramework.Mapping
{
    public class ProductManagementMap
    {
        public static void Configure(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new ProductCategoryMap());
            modelBuilder.Configurations.Add(new ProductStatusMap());
            modelBuilder.Configurations.Add(new ProductGalleryMap());
        }

        private class ProductMap : EntityTypeConfiguration<Product>
        {
            public ProductMap()
            {
                ToTable("Product");
                // Primary Key
                HasKey(t => t.Id);
                Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                // Properties
                Property(t => t.Name).IsRequired();
                Property(t => t.Image).IsOptional();
                Property(t => t.Thumbnail).IsOptional();
                Property(t => t.GalleryId).IsOptional();
                Property(t => t.Price).IsRequired();
                Property(t => t.OldPrice).IsOptional();
                Property(t => t.StatusId).IsOptional();
                Property(t => t.Specifications).IsOptional();
                Property(t => t.CategoryId).IsOptional();
                Property(t => t.Description).IsOptional();

                // Relationships
                HasOptional(t => t.Category).WithMany().HasForeignKey(t => t.CategoryId);
                HasRequired(t => t.Status).WithMany().HasForeignKey(t => t.StatusId);
                HasRequired(t => t.Gallery).WithMany().HasForeignKey(t => t.GalleryId);
            }
        }

        private class ProductStatusMap : EntityTypeConfiguration<ProductStatus>
        {
            public ProductStatusMap()
            {
                ToTable("ProductStatus");
                // Primary Key
                HasKey(t => t.Id);
                Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                // Properties
                Property(t => t.Status).IsRequired();
            }
        }

        private class ProductCategoryMap : EntityTypeConfiguration<ProductCategory>
        {
            public ProductCategoryMap()
            {
                ToTable("ProductCategory");
                // Primary Key
                HasKey(t => t.Id);
                Property(t => t.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                // Properties
                Property(t => t.Name).IsRequired();
                Property(t => t.Description).IsOptional();
                Property(t => t.ParentId).IsOptional();
            }
        }

        private class ProductGalleryMap : EntityTypeConfiguration<ProductGallery>
        {
            public ProductGalleryMap()
            {
                ToTable("ProductGallery");
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