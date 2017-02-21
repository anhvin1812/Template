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
using App.Entities.IdentityManagement;
using App.Entities.ProductManagement;
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

        public IEnumerable<ProductSummary> GetAll(int? page, int? pageSize, ref int? recordCount)
        {
            var products = ProductRepository.GetAll(page, pageSize, ref recordCount)
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
                Specification = product.Specifications,
                Status = product.Status.Status,
                Category = product.Category.Name,
                Price = product.Price,
                OldPrice = product.OldPrice,
                Image = product.Image.Image,
                Thumbnail = product.Image.Thumbnail,
                GalleryThumbnails = product.Gallery.Select(g=>g.Thumbnail).ToList(),
                Gallery = product.Gallery.Select(g=>g.Image).ToList(),
            };
        }

        public ProductUpdateEntry GetProductForEditing(int id)
        {
            var product = ProductRepository.GetById(id);

            if (product == null)
                throw new DataNotFoundException();

            return new ProductUpdateEntry
            {
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
                    Gallery = new List<App.Entities.ProductManagement.Gallery>()
                };

                // upload image
                if (entry.Image != null)
                {
                    var imageName = UploadGallery(entry.Image);

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
                        var fileName = UploadGallery(gallery);

                        entity.Gallery.Add(new Entities.ProductManagement.Gallery
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

            var violations = new List<ErrorExtraInfo>
                {
                    new ErrorExtraInfo {Code = ErrorCodeType.InvalidName, Property = "Name"},
                    new ErrorExtraInfo {Code = ErrorCodeType.InvalidData, Property = "entry"}
                };
            throw new ValidationError(violations);

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
                    var imageName = UploadGallery(entry.Image);

                    if (entity.Image != null)
                    {
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
                        var fileName = UploadGallery(gallery);

                        entity.Gallery.Add(new Entities.ProductManagement.Gallery
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

            return entity.Gallery.Select(x => new GallerySummary
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

            if (entity.Gallery.Any(x=>x.Id == galleryId))
            {
                GalleryService.Delete(galleryId);
            }
        }

        #endregion



        #endregion


        #region Private Methods

        private string UploadGallery(HttpPostedFileBase image)
        {
            var imageName = $"Gallery_{DateTime.Now.ToString("yyyyMMddHHmmss")}{ Path.GetExtension(image.FileName)}";
            Image img = Image.FromStream(image.InputStream);
            Image thumb = img.GetThumbnailImage(270, 270, () => false, IntPtr.Zero);


            img.Save(HttpContext.Current.Server.MapPath($"{Settings.ConfigurationProvider.DirectoryGalleryImage}/{imageName}"));
            thumb.Save(HttpContext.Current.Server.MapPath($"{Settings.ConfigurationProvider.DirectoryGalleryThumbnail}/{imageName}"));

            return imageName;
        }

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
