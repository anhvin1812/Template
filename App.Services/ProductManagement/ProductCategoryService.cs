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
    public class ProductCategoryService : ServiceBase, IProductCategoryService
    {
        #region Contructor
        private IProductCategoryRepository ProductCategoryRepository { get; set; }

        public ProductCategoryService(IUnitOfWork unitOfWork, IProductCategoryRepository productCategoryRepository)
            : base(unitOfWork, new IRepository[] { productCategoryRepository }, new IService[] { })
        {
            ProductCategoryRepository = productCategoryRepository;
        }

        #endregion

        #region Public Methods

        public IEnumerable<ProductCategoryDetail> GetAll(int? page, int? pageSize, ref int? recordCount)
        {
            var categories = ProductCategoryRepository.GetAll(page, pageSize, ref recordCount)
                .Select(x => new ProductCategoryDetail
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    IsDisabled = x.Parent.IsDisabled,
                    Parent = x.Parent == null ? null : new ProductCategoryDetail
                    {
                        Id = x.Parent.Id,
                        Name = x.Parent.Name,
                        Description = x.Parent.Description,
                        IsDisabled = x.Parent.IsDisabled
                    }
                });
            
            return categories;
        }

        public ProductCategoryDetail GetById(int id)
        {
            var category = ProductCategoryRepository.GetById(id);

            if (category == null)
                throw new DataNotFoundException();

            return new ProductCategoryDetail
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                IsDisabled = category.IsDisabled,
                Parent = category.Parent == null ? null : new ProductCategoryDetail
                {
                    Id = category.Parent.Id,
                    Name = category.Parent.Name,
                    Description = category.Parent.Description,
                    IsDisabled = category.IsDisabled
                }
            };
        }


        public void Insert(ProductCategoryEntry entry)
        {
            //TODO: Check exit Role, permission,...

            // Validate data
            //ValidateEntryData(entry);

            //TODO: CHeck parent existed


            var entity = new ProductCategory
            {
                Name = entry.Name,
                Description = entry.Description,
                ParentId = entry.ParentId,
                IsDisabled = entry.IsDisabled
            };

            ProductCategoryRepository.Insert(entity);
            Save();
        }

        public void Update(int id, ProductCategoryEntry entry)
        {
            //TODO: Check exit Role, permission,...

            // Validate data
            //ValidateEntryData(entry);

            //TODO: CHeck parent existed

            var entity = ProductCategoryRepository.GetById(id);
            if (entity == null)
                throw new DataNotFoundException();

            entity.Name = entry.Name;
            entity.Description = entry.Description;
            entity.ParentId = entry.ParentId;
            entity.IsDisabled = entry.IsDisabled;

            ProductCategoryRepository.Update(entity);
            Save();
        }


        #endregion

        #region Private Methods
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
                    ProductCategoryRepository = null;
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion

    }
}
