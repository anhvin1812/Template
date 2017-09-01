
using System;
using System.Collections.Generic;
using System.Linq;
using App.Core.Exceptions;
using App.Core.Repositories;
using App.Entities.NewsManagement;
using App.Repositories.NewsManagement;
using App.Services.Dtos.NewsManagement;
using App.Services.Dtos.UI;
using NewsStatus = App.Core.News.NewsStatus;

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

        public IEnumerable<TagSummary> GetAll(string keyword, bool? isDisabled, int? page, int? pageSize, ref int? recordCount)
        {
            var entities = TagRepository.GetAll(keyword, isDisabled, page, pageSize, ref recordCount);

            return ToDTOs(entities);
        }

        public IEnumerable<TagSummary> GetByStringIds(string ids, bool? isDisabled = null)
        {
            if(string.IsNullOrWhiteSpace(ids))
                return new List<TagSummary>();

            var lstIds = new List<int>();
            try
            {
                lstIds = ids.Split(',').Select(int.Parse).ToList();

                var entities = TagRepository.GetByIds(lstIds);

                if (isDisabled.HasValue)
                    entities = isDisabled.Value
                        ? entities.Where(t => t.IsDisabled == true)
                        : entities.Where(t => t.IsDisabled != true);

                return ToDTOs(entities);
            }
            catch (Exception)
            {

                var violations = new List<ErrorExtraInfo>
                {
                    new ErrorExtraInfo {Code = ErrorCodeType.InvalidData}
                };
                throw new ValidationError(violations);
            }
        }

        public IEnumerable<TagSummary> GetMostUsedTags(bool? isDisabled = null, int maxRecord = 10)
        {
            var entities = TagRepository.GetMostUsedTags(isDisabled, maxRecord);

            return ToDTOs(entities);
        }

        public SelectListOptions GetOptionsForDropdownList(List<int> selectedIds, bool? isDisabled = null)
        {
            var tags = Enumerable.Empty<Tag>();

            if (selectedIds != null && selectedIds.Any())
            {
                tags = TagRepository.GetByIds(selectedIds);
                tags = isDisabled == true ? tags.Where(x => x.IsDisabled == true) : tags.Where(x => x.IsDisabled != true);
            }
            else
            {
                int? recordCount = 0;
                tags = TagRepository.GetAll(null, isDisabled, null, null, ref recordCount);
            }

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
                IsDisabled = entity.IsDisabled ?? false
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

        private IEnumerable<TagSummary> ToDTOs(IEnumerable<Tag> entities, bool? hasCounter = false)
        {
            return entities.Select(x => new TagSummary
            {
                Id = x.Id,
                Name = x.Name,
                IsDisabled = x.IsDisabled ?? false,
                NewsCount = hasCounter == true ? x.Newses.Count(n=>n.StatusId == (int)NewsStatus.Public) : 0
            });
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
