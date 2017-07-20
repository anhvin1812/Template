using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using App.Core.Configuration;
using App.Core.Exceptions;
using App.Core.Repositories;
using App.Entities;
using App.Entities.ProductManagement;
using App.Entities.ProductManagement;
using App.Infrastructure.File;
using App.Repositories.ProductManagement;
using App.Services.Dtos.Gallery;
using App.Services.Dtos.ProductManagement;
using App.Services.Gallery;
using Gallery= App.Entities.ProductManagement.Gallery;

namespace App.Services.ProductManagement
{
    public class ProductService : ServiceBase, IProductService
    {
        #region Contructor
        private IProductRepository ProductRepository { get; set; }
        private IGalleryService GalleryService { get; set; }

        public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository, IGalleryService galleryService)
            : base(unitOfWork, new IRepository[] { productRepository }, new IService[] { galleryService })
        {
            ProductRepository = productRepository;
            GalleryService = galleryService;
        }

        #endregion

        #region Public Methods

        public IEnumerable<ProductSummary> GetAll(string keyword, int? categoryId, int? page, int? pageSize, ref int? recordCount)
        {
            var products = ProductRepository.GetAll(keyword, categoryId, page, pageSize, ref recordCount)
                .Select(x => new ProductSummary
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    OldPrice = x.OldPrice,
                    Status = x.Status.Status,
                    Thumbnail = x.Image.Thumbnail,
                    Category = x.Category.Name
                });

            return products;
        }

        public IEnumerable<ProductSummary> GetRelatedProducts(int productId, int categoryId, int? maxRecords = null)
        {
            var products = ProductRepository.GetRelatedProducts(productId, categoryId, maxRecords)
                .Select(x => new ProductSummary
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    OldPrice = x.OldPrice,
                    Status = x.Status.Status,
                    Thumbnail = x.Image.Thumbnail,
                    Category = x.Category.Name
                });

            return products;
        }

        public ProductDetail GetById(int id)
        {
            var product = ProductRepository.GetById(id);

            if (product == null)
                throw new DataNotFoundException();

            return new ProductDetail
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Specifications = product.Specifications,
                Status = product.Status.Status,
                CategoryId = product.CategoryId,
                Category = product.Category.Name,
                Price = product.Price,
                OldPrice = product.OldPrice,
                Image = product.Image.Image,
                Thumbnail = product.Image.Thumbnail,
                Galleries = product.Galleries.Select(g=> new GallerySummary{
                    Id = g.Id,
                    Image = g.Image,
                    Thumbnail = g.Thumbnail
                }).ToList()
            };
        }

        public ProductUpdateEntry GetProductForEditing(int id)
        {
            var product = ProductRepository.GetById(id);

            if (product == null)
                throw new DataNotFoundException();

            return new ProductUpdateEntry
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Specifications = product.Specifications,
                StatusId = product.StatusId,
                CategoryId = product.CategoryId,
                Price = product.Price,
                OldPrice = product.OldPrice,
                Thumbnail = product.Image.Thumbnail,
            };
        }

        public void Insert(ProductEntry entry)
        {
            //TODO: Check exit Role, permission,...

            // Validate data
            ValidateEntryData(entry);

            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required))
            {
                var entity = new Product
                {
                    Name = entry.Name,
                    Description = entry.Description,
                    Specifications = entry.Specifications,
                    Price = entry.Price,
                    OldPrice = entry.OldPrice,
                    CategoryId = entry.CategoryId,
                    StatusId = entry.StatusId,
                    Galleries = new List<App.Entities.ProductManagement.Gallery>()
                };

                // upload image
                if (entry.Image != null)
                {
                    var imageName = GalleryHelper.UploadGallery(entry.Image, Settings.ConfigurationProvider.ThumbnailWidth);

                    entity.Image = new Entities.ProductManagement.Gallery
                    {
                        Image = imageName,
                        Thumbnail = imageName,
                        State = ObjectState.Added
                    };
                }

                // upload gallery
                if (entry.Gallery != null && entry.Gallery.Any())
                {
                    foreach (var gallery in entry.Gallery)
                    {
                        var fileName = GalleryHelper.UploadGallery(gallery, Settings.ConfigurationProvider.ThumbnailWidth);

                        entity.Galleries.Add(new Entities.ProductManagement.Gallery
                        {
                            Image = fileName,
                            Thumbnail = fileName,
                            State = ObjectState.Added
                        });
                    }
                }

                
                ProductRepository.Insert(entity);
                Save();

                transactionScope.Complete();
            }
          
        }

        public void Update(int id, ProductUpdateEntry entry)
        {
            //TODO: Check exit Role, permission,...

            // Validate data
            ValidateUpdateEntryData(entry);

            var entity = ProductRepository.GetById(id);
            if(entity == null)
                throw new DataNotFoundException();

            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required))
            {
                entity.Name = entry.Name;
                entity.Description = entry.Description;
                entity.Specifications = entry.Specifications;
                entity.Price = entry.Price;
                entity.OldPrice = entry.OldPrice;
                entity.CategoryId = entry.CategoryId;
                entity.StatusId = entry.StatusId;
                
                // upload image
                if (entry.Image != null)
                {
                    var imageName = GalleryHelper.UploadGallery(entry.Image, Settings.ConfigurationProvider.ThumbnailWidth);

                    if (entity.Image != null)
                    {
                        GalleryHelper.DeleteGallery(entity.Image.Image, entity.Image.Thumbnail);

                        entity.Image.Image = imageName;
                        entity.Image.Thumbnail = imageName;
                        entity.Image.State = ObjectState.Modified;
                    }
                    else
                    {
                        entity.Image = new Entities.ProductManagement.Gallery
                        {
                            Image = imageName,
                            Thumbnail = imageName,
                            State = ObjectState.Added
                        };
                    }
                    
                }

                // upload gallery
                if (entry.Gallery != null && entry.Gallery.Any())
                {
                    foreach (var gallery in entry.Gallery)
                    {
                        if (gallery == null) continue;

                        var fileName = GalleryHelper.UploadGallery(gallery, Settings.ConfigurationProvider.ThumbnailWidth);

                        entity.Galleries.Add(new Entities.ProductManagement.Gallery
                        {
                            Image = fileName,
                            Thumbnail = fileName,
                            State = ObjectState.Added
                        });
                    }
                }

                ProductRepository.Update(entity);
                Save();

                transactionScope.Complete();
            }
        }


        #region Product Status

        public IEnumerable<ProductStatusSummary> GetAllStatus()
        {
            var results = ProductRepository.GetAllStatus();

            return results.Select(x => new ProductStatusSummary
            {
                Id = x.Id,
                Status = x.Status
            });
        }
        #endregion

        #region Products Gallery

        public IEnumerable<GallerySummary> GetGalleryByProductId(int id)
        {
            var entity = ProductRepository.GetById(id);
            if(entity==null)
                throw new DataNotFoundException();

            return entity.Galleries.Select(x => new GallerySummary
            {
                Id = x.Id,
                Image = x.Image,
                Thumbnail = x.Thumbnail
            });
        }

        public void DeleteProductGallery(int productId, int galleryId)
        {
            var entity = ProductRepository.GetById(productId);
            if (entity == null)
                throw new DataNotFoundException();

            var gallery = entity.Galleries.FirstOrDefault(x => x.Id == galleryId);
            if (gallery != null)
            {
                entity.Galleries.Remove(gallery);
                GalleryService.Delete(galleryId);

                Save();
            }
        }

        #endregion



        #endregion


        #region Private Methods

        private void ValidateEntryData(ProductEntry entry)
        {

            if (entry == null)
            {
                var violations = new List<ErrorExtraInfo>
                {
                    new ErrorExtraInfo {Code = ErrorCodeType.InvalidData}
                };
                throw new ValidationError(violations);
            }

            if ( string.IsNullOrWhiteSpace(entry.Name) )
            {
                var violations = new List<ErrorExtraInfo>
                {
                    new ErrorExtraInfo {Code = ErrorCodeType.InvalidName, Property = "Name"}
                };
                throw new ValidationError(violations);
            }
        }

        private void ValidateUpdateEntryData(ProductUpdateEntry entry)
        {
            if (entry == null)
            {
                var violations = new List<ErrorExtraInfo>
                {
                    new ErrorExtraInfo {Code = ErrorCodeType.InvalidData}
                };
                throw new ValidationError(violations);
            }

            if (string.IsNullOrWhiteSpace(entry.Name))
            {
                var violations = new List<ErrorExtraInfo>
                {
                    new ErrorExtraInfo {Code = ErrorCodeType.InvalidName, Property = "Name"}
                };
                throw new ValidationError(violations);
            }

        }

        #endregion


        #region Dispose
        private bool _disposed = false;

        protected override void Dispose(bool isDisposing)
        {
            if (!this._disposed)
            {
                if (isDisposing)
                {
                    ProductRepository = null;
                    GalleryService = null;
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion

    }
}
