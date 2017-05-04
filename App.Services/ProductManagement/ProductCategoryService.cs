using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI.WebControls;
using App.Core.Configuration;
using App.Core.Exceptions;
using App.Core.Repositories;
using App.Entities;
using App.Entities.ProductManagement;
using App.Entities.ProductManagement;
using App.Repositories.IdentityManagement;
using App.Repositories.ProductManagement;
using App.Services.Dtos.IdentityManagement;
using App.Services.Dtos.ProductManagement;
using App.Services.Dtos.UI;

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

        public IEnumerable<ProductCategorySummary> GetAll(int? page, int? pageSize, ref int? recordCount)
        {
            var categories = ProductCategoryRepository.GetAll(page, pageSize, ref recordCount)
                .Select(x => new ProductCategorySummary
                {
                    Id = x.Id,
                    Name = x.Name,
                    ParentId = x.ParentId,
                    Description = x.Description,
                    IsDisabled = x.IsDisabled ?? false
                });

            return categories;
        }

        /// <summary>
        /// parrentId is null to get all categories
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="currentId">to disable all under categories and itself</param>
        /// <param name="isDisabled"></param>
        /// <returns></returns>
        public SelectListOptions GetOptionsForDropdownList(int? parentId, int? currentId = null, bool? isDisabled = null)
        {
            int? recordCount = 0;
            var allCategories = ProductCategoryRepository.GetAll(null, null, ref recordCount);

            allCategories = isDisabled.HasValue ?
                isDisabled.Value ? allCategories.Where(x => x.IsDisabled == true) : allCategories.Where(x => x.IsDisabled != true)
                : allCategories;

            var results = GetCategoryForDropdown(allCategories, parentId, string.Empty);
            var disabledValues = new List<int>();

            if (currentId.HasValue)
            {
                disabledValues.Add(currentId.Value);
                disabledValues.AddRange(GetCategoryForDropdown(results, currentId.Value, string.Empty).Select(x=>x.Id).ToList());
            }

            return new SelectListOptions
            {
                Items = results.Select(x=> new OptionItem {Value = x.Id, Text = x.Name}),
                DisabledValues = disabledValues
            };
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

        public ProductCategoryEntry GetCategoryForEditing(int id)
        {
            var category = ProductCategoryRepository.GetById(id);

            if (category == null)
                throw new DataNotFoundException();

            return new ProductCategoryEntry
            {
                Name = category.Name,
                Description = category.Description,
                IsDisabled = category.IsDisabled,
               ParentId = category.ParentId
            };
        }


        public void Insert(ProductCategoryEntry entry)
        {
            //TODO: Check exit Role, permission,...

            // Validate data
            ValidateEntryData(entry);

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
            ValidateEntryData(entry);

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
        private void ValidateEntryData(ProductCategoryEntry entry)
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

        private List<ProductCategory> GetCategoryForDropdown(IEnumerable<ProductCategory> allCategories, int? parentId, string paddingString)
        {
            var categories = new List<ProductCategory>();

            var rootCategories = allCategories.Where(x => x.ParentId == parentId);
            if (rootCategories.Any())
            {
                foreach (var c in rootCategories)
                {
                    c.Name = HttpUtility.HtmlDecode(paddingString + c.Name);
                    categories.Add(c);

                    if (allCategories.Any(x => x.ParentId == c.Id))
                    {
                        categories.AddRange(GetCategoryForDropdown(allCategories, c.Id, $"{paddingString}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"));
                    }
                }
            }

            return categories;
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
