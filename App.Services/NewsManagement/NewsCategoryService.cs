using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.Core.Exceptions;
using App.Core.Repositories;
using App.Entities.NewsManagement;
using App.Entities.ProductManagement;
using App.Repositories.NewsManagement;
using App.Repositories.ProductManagement;
using App.Services.Dtos.NewsManagement;
using App.Services.Dtos.ProductManagement;
using App.Services.Dtos.UI;

namespace App.Services.NewsManagement
{
    public class NewsCategoryService : ServiceBase, INewsCategoryService
    {
        #region Contructor
        private INewsCategoryRepository NewsCategoryRepository { get; set; }

        public NewsCategoryService(IUnitOfWork unitOfWork, INewsCategoryRepository newsCategoryRepository)
            : base(unitOfWork, new IRepository[] { newsCategoryRepository }, new IService[] { })
        {
            NewsCategoryRepository = newsCategoryRepository;
        }

        #endregion

        #region Public Methods

        public IEnumerable<NewsCategorySummary> GetAll(int? page, int? pageSize, ref int? recordCount)
        {
            var categories = NewsCategoryRepository.GetAll(page, pageSize, ref recordCount)
                .Select(x => new NewsCategorySummary
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
            var allCategories = NewsCategoryRepository.GetAll(null, null, ref recordCount);

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
                Items = results.Select(x => new OptionItem { Value = x.Id, Text = x.Name }),
                DisabledValues = disabledValues
            };
        }

        public NewsCategoryDetail GetById(int id)
        {
            var category = NewsCategoryRepository.GetById(id);

            if (category == null)
                throw new DataNotFoundException();

            return new NewsCategoryDetail
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                IsDisabled = category.IsDisabled,
                Parent = category.Parent == null ? null : new NewsCategoryDetail
                {
                    Id = category.Parent.Id,
                    Name = category.Parent.Name,
                    Description = category.Parent.Description,
                    IsDisabled = category.IsDisabled
                }
            };
        }

        public NewsCategoryEntry GetCategoryForEditing(int id)
        {
            var category = NewsCategoryRepository.GetById(id);

            if (category == null)
                throw new DataNotFoundException();

            return new NewsCategoryEntry
            {
                Name = category.Name,
                Description = category.Description,
                IsDisabled = category.IsDisabled,
               ParentId = category.ParentId
            };
        }


        public void Insert(NewsCategoryEntry entry)
        {
            //TODO: Check exit Role, permission,...

            // Validate data
            ValidateEntryData(entry);

            //TODO: CHeck parent existed


            var entity = new NewsCategory
            {
                Name = entry.Name,
                Description = entry.Description,
                ParentId = entry.ParentId,
                IsDisabled = entry.IsDisabled
            };

            NewsCategoryRepository.Insert(entity);
            Save();
        }

        public void Update(int id, NewsCategoryEntry entry)
        {
            //TODO: Check exit Role, permission,...

            // Validate data
            ValidateEntryData(entry);

            //TODO: CHeck parent existed

            var entity = NewsCategoryRepository.GetById(id);
            if (entity == null)
                throw new DataNotFoundException();

            entity.Name = entry.Name;
            entity.Description = entry.Description;
            entity.ParentId = entry.ParentId;
            entity.IsDisabled = entry.IsDisabled;

            NewsCategoryRepository.Update(entity);
            Save();
        }


        #endregion

        #region Private Methods
        private void ValidateEntryData(NewsCategoryEntry entry)
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

        private List<NewsCategory> GetCategoryForDropdown(IEnumerable<NewsCategory> allCategories, int? parentId, string paddingString)
        {
            var categories = new List<NewsCategory>();

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
                    NewsCategoryRepository = null;
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion

    }
}
