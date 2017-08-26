using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using App.Core.Configuration;
using App.Core.Exceptions;
using App.Core.News;
using App.Core.Repositories;
using App.Entities;
using App.Entities.NewsManagement;
using App.Entities.ProductManagement;
using App.Infrastructure.File;
using App.Repositories.NewsManagement;
using App.Repositories.ProductManagement;
using App.Services.Common;
using App.Services.Dtos.Gallery;
using App.Services.Dtos.NewsManagement;
using App.Services.Dtos.ProductManagement;
using App.Services.Dtos.UI;
using App.Services.Gallery;
using NewsStatus = App.Core.News.NewsStatus;

namespace App.Services.NewsManagement
{
    public class NewsService : ServiceBase, INewsService
    {
        #region Contructor
        private INewsRepository NewsRepository { get; set; }
        private INewsCategoryRepository NewsCategoryRepository { get; set; }
        private ITagRepository TagRepository { get; set; }
        private IGalleryService GalleryService { get; set; }

        public NewsService(IUnitOfWork unitOfWork, INewsRepository newsRepository, INewsCategoryRepository newsCategoryRepository, ITagRepository tagRepository,
            IGalleryService galleryService)
            : base(unitOfWork, new IRepository[] { newsRepository, newsCategoryRepository, tagRepository }, 
                  new IService[] { galleryService })
        {
            NewsRepository = newsRepository;
            NewsCategoryRepository = newsCategoryRepository;
            TagRepository = tagRepository;

            GalleryService = galleryService;
        }

        #endregion

        #region Public Methods

        public IEnumerable<NewsSummary> GetAll(string keyword, int? categoryId, int? statusId, int? mediaTypeId, bool? hot, bool? featured, int? page, int? pageSize, ref int? recordCount)
        {
            var news = NewsRepository.GetAll(keyword, categoryId, statusId, mediaTypeId, hot, featured, page, pageSize, ref recordCount);
            
            return EntitiesToDtos(news);
        }

        public IEnumerable<NewsSummary> GetRelatedNews(int newsId, int categoryId, int? maxRecords = null)
        {
            var news = NewsRepository.GetRelatedNews(newsId, categoryId, maxRecords);

            return EntitiesToDtos(news);
        }

        public NewsDetail GetById(int id)
        {
            var news = NewsRepository.GetById(id);

            if (news == null)
                throw new DataNotFoundException();

            return EntityToDto(news);
        }

        public NewsUpdateEntry GetEntryForEditing(int id)
        {
            var news = NewsRepository.GetById(id);

            if (news == null)
                throw new DataNotFoundException();

            return new NewsUpdateEntry
            {
                Id = news.Id,
                Title = news.Title,
                Description = news.Description,
                Content = news.Content,
                Thumbnail = news.Image.Thumbnail,
                IsHot = news.IsHot ?? false,
                IsFeatured = news.IsFeatured ?? false,
                StatusId = news.StatusId,
                CategoryIds = news.Categories.Where(x=>x.IsDisabled != true).Select(x=>x.Id).ToList(),
                TagIds = news.Tags.Where(x=>x.IsDisabled != true).Select(x=>x.Id).ToList()
            };
        }

        public void Insert(NewsEntry entry)
        {
            //TODO: Check exit Role, permission,...

            // Validate data
            ValidateEntryData(entry);

            var userId = CurrentClaimsIdentity.GetUserId();

            // status
            var status = NewsRepository.GetStatusById(entry.StatusId);
            if (status == null)
            {
                var violations = new List<ErrorExtraInfo>
                {
                    new ErrorExtraInfo {Code = ErrorCodeType.InvalidName, Property = "Status"}
                };
                throw new ValidationError(violations);
            }

            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required))
            {
                var entity = new News
                {
                    Title = entry.Title,
                    Description = entry.Description,
                    Content = entry.Content,
                    IsHot = entry.IsHot,
                    IsFeatured = entry.IsFeatured,
                    StatusId = entry.StatusId,
                    MediaTypeId = entry.MediaTypeId,
                    CreatedDate = DateTime.Now,
                    UpdatedById = userId,
                    
                    Categories = new List<NewsCategory>(),
                    Tags = new List<Tag>()
                };

                // categories
                if(entry.CategoryIds != null && entry.CategoryIds.Any())
                {
                    var categories = NewsCategoryRepository.GetByIds(entry.CategoryIds).ToList();
                    entity.Categories = categories;
                }

                // tags
                if (entry.TagIds != null && entry.TagIds.Any())
                {
                    var tags = TagRepository.GetByIds(entry.TagIds).ToList();
                    entity.Tags = tags;
                }

                // upload image
                if (entry.Image != null)
                {
                    var thumbnailWidth = entry.MediaTypeId == (int) MediaType.Photo
                        ? AppSettings.ConfigurationProvider.ThumbnailWidth
                        : AppSettings.ConfigurationProvider.ThumbnailPhotoWidth;

                    var imageName = GalleryHelper.UploadGallery(entry.Image, thumbnailWidth);

                    entity.Image = new Entities.ProductManagement.Gallery
                    {
                        Image = imageName,
                        Thumbnail = imageName,
                        State = ObjectState.Added
                    };
                }
                
                NewsRepository.Insert(entity);
                Save();

                transactionScope.Complete();
            }
          
        }

        public void Update(int id, NewsUpdateEntry entry)
        {
            //TODO: Check exit Role, permission,...

            // Validate data
            ValidateUpdateEntryData(entry);

            var userId = CurrentClaimsIdentity.GetUserId();

            var entity = NewsRepository.GetById(id);
            if(entity == null)
                throw new DataNotFoundException();

            // status
            var status = NewsRepository.GetStatusById(entry.StatusId);
            if(status == null)
            {
                var violations = new List<ErrorExtraInfo>
                {
                    new ErrorExtraInfo {Code = ErrorCodeType.InvalidName, Property = "Status"}
                };
                throw new ValidationError(violations);
            }

            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required))
            {
                entity.Title = entry.Title;
                entity.Description = entry.Description;
                entity.Content = entry.Content;
                entity.IsHot = entry.IsHot;
                entity.IsFeatured = entry.IsFeatured;
                entity.StatusId = entry.StatusId;
                entity.MediaTypeId = entry.MediaTypeId;

                entity.UpdatedDate = DateTime.Now;
                entity.UpdatedById = userId;

                // categories
                if (entry.CategoryIds != null && entry.CategoryIds.Any())
                {
                    var categories = NewsCategoryRepository.GetByIds(entry.CategoryIds).ToList();

                    entity.Categories.Clear();
                    entity.Categories = categories;
                }

                // tags
                if (entry.TagIds != null && entry.TagIds.Any())
                {
                    var tags = TagRepository.GetByIds(entry.TagIds).ToList();

                    entity.Tags.Clear();
                    entity.Tags = tags;
                }

                // upload image
                if (entry.Image != null)
                {
                    var thumbnailWidth = entry.MediaTypeId == (int)MediaType.Photo
                        ? AppSettings.ConfigurationProvider.ThumbnailWidth
                        : AppSettings.ConfigurationProvider.ThumbnailPhotoWidth;

                    var imageName = GalleryHelper.UploadGallery(entry.Image, thumbnailWidth);

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

                NewsRepository.Update(entity);
                Save();

                transactionScope.Complete();
            }
        }

        public void Delete(int id)
        {
            //TODO: Check exit Role, permission,...

            var userId = CurrentClaimsIdentity.GetUserId();

            var entity = NewsRepository.GetById(id);
            if (entity == null)
                throw new DataNotFoundException();

            entity.DeletedDate = DateTime.Now;
            entity.UpdatedById = userId;

            GalleryService.Delete(entity.Image.Id);

            NewsRepository.Update(entity);
            Save();
        }

        public PublicNewsDetail Preview(NewsUpdateEntry entry)
        {
            var detail = new PublicNewsDetail
            {
                Id = entry.Id,
                Title = entry.Title,
                Description = entry.Description,
                Content = entry.Content,
                MediaTypeId = entry.MediaTypeId,
                PublishedDate = DateTime.Now,
                Views = 0,
                
            };
            if (entry.Image != null)
                detail.Image = entry.Image.ToBase64String();

            if (entry.CategoryIds != null && entry.CategoryIds.Any())
            {
                var categories = NewsCategoryRepository.GetByIds(entry.CategoryIds).ToList();
                detail.Categories = categories.Select(x => new PublicCategorySummary
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
            }

            if (entry.TagIds != null && entry.TagIds.Any())
            {
                var tags = TagRepository.GetByIds(entry.TagIds).ToList();
                detail.Tags = tags.Select(x => new PublicTagSummary
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
            }

            return detail;
        }

        #region Status

        public SelectListOptions GetStatusOptionsForDropdownList()
        {
            var allStatuses = NewsRepository.GetAllStatus();

            return new SelectListOptions
            {
                Items = allStatuses.Select(x => new OptionItem { Value = x.Id, Text = x.Status }),
            };
        }

        #endregion

        #region Media type
        public SelectListOptions GetMediaTypeOptionsForDropdownList()
        {
            var status = Enum.GetValues(typeof (MediaType)).OfType<MediaType>();
            
            return new SelectListOptions
            {
                Items = status.Select(x => new OptionItem { Value = (int)x, Text = x.ToString() }),
            };
        }
        #endregion

        #endregion


        #region Private Methods

        private void ValidateEntryData(NewsEntry entry)
        {
            var violations = new List<ErrorExtraInfo>();

            if (entry == null)
            {
                violations.Add(new ErrorExtraInfo { Code = ErrorCodeType.InvalidData });
                throw new ValidationError(violations);
            }

            if ( string.IsNullOrWhiteSpace(entry.Title) )
            {
                violations.Add(new ErrorExtraInfo { Code = ErrorCodeType.InvalidTitle, Property = "Title" });
            }

            if (entry.Image == null )
            {
                violations.Add(new ErrorExtraInfo { Code = ErrorCodeType.EmptyImage, Property = "Image" });
            }

            if (entry.CategoryIds == null || !entry.CategoryIds.Any())
            {
                violations.Add(new ErrorExtraInfo { Code = ErrorCodeType.NewsCategoryIsEmpty, Property = "CategoryIds" });
            }


            if (violations.Any())
                throw new ValidationError(violations);

        }

        private void ValidateUpdateEntryData(NewsUpdateEntry entry)
        {
            var violations = new List<ErrorExtraInfo>();

            if (entry == null)
            {
                violations.Add(new ErrorExtraInfo { Code = ErrorCodeType.InvalidData });
                throw new ValidationError(violations);
            }

            if (string.IsNullOrWhiteSpace(entry.Title))
            {
                violations.Add(new ErrorExtraInfo { Code = ErrorCodeType.InvalidTitle, Property = "Title" });
            }

            if (entry.CategoryIds == null || !entry.CategoryIds.Any())
            {
                violations.Add(new ErrorExtraInfo { Code = ErrorCodeType.NewsCategoryIsEmpty, Property = "CategoryIds" });
            }


            if (violations.Any())
                throw new ValidationError(violations);

        }

        #endregion

        #region EntityMap
        private IEnumerable<NewsSummary> EntitiesToDtos(IEnumerable<News> entities)
        {
            return entities.Select(x => new NewsSummary
            {
                Id = x.Id,
                Title = x.Title,
                Views = x.Views,
                Status = x.Status.Status,
                MediaTypeId = x.MediaTypeId,
                IsHot = x.IsHot ?? false,
                IsFeatured = x.IsFeatured ?? false,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                UpdatedBy = $"{x.Editor.Firstname} {x.Editor.Lastname}",
                Categories = x.Categories?.Select(c=>c.Name).ToList(),
                Tags = x.Tags?.Select(t=>t.Name).ToList()
            });
        }

        private NewsDetail EntityToDto(News entity)
        {
            return new NewsDetail
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Thumbnail = entity.Image.Thumbnail,
                Image = entity.Image.Image,
                Content = entity.Content,
                IsHot = entity.IsHot ?? false,
                IsFeatured = entity.IsFeatured ?? false,
                StatusId = entity.StatusId,
                MediaTypeId = entity.MediaTypeId,
                Views = entity.Views,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                UpdatedBy = entity.Editor.Firstname
            };
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
                    NewsRepository = null;
                    NewsCategoryRepository = null;
                    TagRepository = null;
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion

    }
}
