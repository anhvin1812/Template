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
using App.Repositories.IdentityManagement;
using App.Repositories.ProductManagement;
using App.Services.Dtos.IdentityManagement;
using App.Services.Dtos.ProductManagement;

namespace App.Services.ProductManagement
{
    public class ProductService : ServiceBase, IProductService
    {
        #region Contructor
        private IProductRepository ProductRepository { get; set; }

        public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository)
            : base(unitOfWork, new IRepository[] { productRepository }, new IService[] { })
        {
            ProductRepository = productRepository;
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
                    Specifications = entry.Specification,
                    Price = entry.Price,
                    OldPrice = entry.OldPrice,
                    CategoryId = entry.CategoryId,
                    StatusId = entry.StatusId,
                    Gallery = new List<Gallery>()
                };

                // upload image
                if (entry.Image != null)
                {
                    var imageName = UploadGallery(entry.Image);

                    entity.Image = new Gallery
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

                        entity.Gallery.Add(new Gallery
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
            //ValidateEntryData(entry);

            //var roleEntity = RoleRepository.GetById(id);

            //// Validate entity
            //ValidateEntityData(roleEntity);

            //// Check existed name
            //var roleForValidation = RoleRepository.GetByName(entry.RoleName);
            //if (roleForValidation != null && roleForValidation.Id != roleEntity.Id)
            //{
            //    var violations = new List<ErrorExtraInfo>
            //    {
            //        new ErrorExtraInfo {Code = ErrorCodeType.RoleNameIsUsed, Property = "RoleName"}
            //    };
            //    throw new ValidationError(violations);
            //}

            //var roleClaims = entry.RoleClaims.Where(x => x.IsChecked);
            //using (var transactionScope = new TransactionScope(TransactionScopeOption.Required))
            //{
            //    // Remmove old claims
            //    var oldClaims = RoleRepository.GetRoleClaimsByRoleId(roleEntity.Id);
            //    foreach (var claim in oldClaims)
            //    {
            //        RoleRepository.DeleteRoleClaim(claim.Id);
            //    }

            //    // Add new claims into role
            //    if (roleClaims != null && roleClaims.Any())
            //    {
            //        foreach (var roleClaim in roleClaims)
            //        {
            //            RoleRepository.InsertRoleClaim(new RoleClaim
            //            {
            //                RoleId = roleEntity.Id,
            //                ClaimType = roleClaim.ClaimType,
            //                ClaimValue = roleClaim.ClaimValue
            //            });
            //        }
            //    }
            //    // Update role
            //    roleEntity.Name = entry.RoleName;
            //    roleEntity.Description = entry.Description;

            //    RoleRepository.Update(roleEntity);
            //    Save();

            //    transactionScope.Complete();
            //}
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



        #endregion


        #region Private Methods

        private string UploadProductImage(HttpPostedFileBase image)
        {
            var imageName = $"Product_{DateTime.Now.ToString("yyyyMMddHHmmss")}.{ Path.GetExtension(image.FileName)}";
            Image img = Image.FromStream(image.InputStream);
            Image thumb = img.GetThumbnailImage(270, 270, () => false, IntPtr.Zero);


            img.Save(HttpContext.Current.Server.MapPath($"{Settings.ConfigurationProvider.DirectoryProductImage}/{imageName}"));
            thumb.Save(HttpContext.Current.Server.MapPath($"{Settings.ConfigurationProvider.DirectoryProductThumbnail}/{imageName}"));

            return imageName;
        }

        private string UploadGallery(HttpPostedFileBase image)
        {
            var imageName = $"Gallery_{DateTime.Now.ToString("yyyyMMddHHmmss")}.{ Path.GetExtension(image.FileName)}";
            Image img = Image.FromStream(image.InputStream);
            Image thumb = img.GetThumbnailImage(270, 270, () => false, IntPtr.Zero);


            img.Save(HttpContext.Current.Server.MapPath($"{Settings.ConfigurationProvider.DirectoryGalleryImage}/{imageName}"));
            thumb.Save(HttpContext.Current.Server.MapPath($"{Settings.ConfigurationProvider.DirectoryGalleryThumbnail}/{imageName}"));

            return imageName;
        }



        private bool IsChecked(RoleClaim roleClaim, List<RoleClaimSummary> allPermissions)
        {
            return allPermissions.Any(x => x.ClaimType == roleClaim.ClaimType && x.ClaimValue == roleClaim.ClaimValue);
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

        private void ValidateEntityData(Role entity)
        {
            if (entity == null)
            {
                var violations = new List<ErrorExtraInfo>
                {
                    new ErrorExtraInfo {Code = ErrorCodeType.RoleIsNotExsted}
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
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion

    }
}
