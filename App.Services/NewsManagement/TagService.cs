
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
    public class TagService : ServiceBase, ITagService
    {
        #region Contructor
        private ITagRepository TagRepository { get; set; }

        public TagService(IUnitOfWork unitOfWork, ITagRepository tagRepository)
            : base(unitOfWork, new IRepository[] { tagRepository }, new IService[] { })
        {
            TagRepository = tagRepository;
        }

        #endregion

        #region Public Methods

        public IEnumerable<TagSummary> GetAll(int? page, int? pageSize, ref int? recordCount)
        {
            var categories = TagRepository.GetAll(page, pageSize, ref recordCount)
                .Select(x => new TagSummary
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsDisabled = x.IsDisabled ?? false
                });

            return categories;
        }

        public SelectListOptions GetOptionsForDropdownList(bool? isDisabled = null)
        {
            int? recordCount = 0;
            var tags = TagRepository.GetAll(null, null, ref recordCount);

            tags = isDisabled.HasValue ?
                isDisabled.Value ? tags.Where(x => x.IsDisabled == true) : tags.Where(x => x.IsDisabled != true)
                : tags;

            return new SelectListOptions
            {
                Items = tags.Select(x => new OptionItem { Value = x.Id, Text = x.Name }),
            };
        }

        public TagDetail GetById(int id)
        {
            var entity = TagRepository.GetById(id);

            if (entity == null)
                throw new DataNotFoundException();

            return new TagDetail
            {
                Id = entity.Id,
                Name = entity.Name,
                IsDisabled = entity.IsDisabled ?? false,
            };
        }

        public TagEntry GetEntryForEditing(int id)
        {
            var entity = TagRepository.GetById(id);

            if (entity == null)
                throw new DataNotFoundException();

            return new TagEntry
            {
                Name = entity.Name,
                IsDisabled = entity.IsDisabled
            };
        }


        public void Insert(TagEntry entry)
        {
            //TODO: Check exit Role, permission,...

            // Validate data
            ValidateEntryData(entry);

            // check duplicating tag
            var tagWithSameName = TagRepository.GetByName(entry.Name);
            if(tagWithSameName != null)
            {
                var violations = new List<ErrorExtraInfo>
                {
                    new ErrorExtraInfo {Code = ErrorCodeType.TagNameIsExisted}
                };
                throw new ValidationError(violations);
            }

            var entity = new Tag
            {
                Name = entry.Name,
                IsDisabled = entry.IsDisabled
            };

            TagRepository.Insert(entity);
            Save();
        }

        public void Update(int id, TagEntry entry)
        {
            //TODO: Check exit Role, permission,...

            // Validate data
            ValidateEntryData(entry);

            var entity = TagRepository.GetById(id);
            if (entity == null)
                throw new DataNotFoundException();

            // check duplicating tag
            var tagWithSameName = TagRepository.GetByName(entry.Name);
            if (tagWithSameName != null && tagWithSameName.Id != entity.Id)
            {
                var violations = new List<ErrorExtraInfo>
                {
                    new ErrorExtraInfo {Code = ErrorCodeType.TagNameIsExisted}
                };
                throw new ValidationError(violations);
            }

            entity.Name = entry.Name;
            entity.IsDisabled = entry.IsDisabled;

            TagRepository.Update(entity);
            Save();
        }


        #endregion

        #region Private Methods
        private void ValidateEntryData(TagEntry entry)
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

        #endregion


        #region Dispose
        private bool _disposed = false;

        protected override void Dispose(bool isDisposing)
        {
            if (!this._disposed)
            {
                if (isDisposing)
                {
                    TagRepository = null;
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion

    }
}
