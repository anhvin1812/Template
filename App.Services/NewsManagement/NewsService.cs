using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using App.Core.Exceptions;
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
using App.Services.Gallery;

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

        public IEnumerable<NewsSummary> GetAll(string keyword, int? categoryId, int? page, int? pageSize, ref int? recordCount)
        {
            var news = NewsRepository.GetAll(keyword, categoryId, page, pageSize, ref recordCount);
            
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
                IsDisabled = news.IsDisabled ?? false,
                CategoryIds = news.Categories.Where(x=>x.IsDisabled != false).Select(x=>x.Id).ToList(),
                TagIds = news.Tags.Where(x=>x.IsDisabled != false).Select(x=>x.Id).ToList()
            };
        }

        public void Insert(NewsEntry entry)
        {
            //TODO: Check exit Role, permission,...

            // Validate data
            ValidateEntryData(entry);

            var userId = CurrentClaimsIdentity.GetUserId();

            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required))
            {
                var entity = new News
                {
                    Title = entry.Title,
                    Description = entry.Description,
                    Content = entry.Content,
                    IsHot = entry.IsHot,
                    IsFeatured = entry.IsFeatured,
                    IsDisabled = entry.IsFeatured,
                    CreatedDate = DateTime.Now,
                    UpdatedById = userId,
                    
                    Categories = new List<NewsCategory>(),
                    Tags = new List<Tag>()
                };

                // categories
                if(entry.CategoryIds != null && entity.Categories.Any())
                {
                    var categories = NewsCategoryRepository.GetByIds(entry.CategoryIds).ToList();
                    entity.Categories = categories;
                }

                // tags
                if (entry.TagIds != null && entity.Tags.Any())
                {
                    var tags = TagRepository.GetByIds(entry.TagIds).ToList();
                    entity.Tags = tags;
                }

                // upload image
                if (entry.Image != null)
                {
                    var imageName = GalleryHelper.UploadGallery(entry.Image);

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

            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required))
            {
                entity.Title = entry.Title;
                entity.Description = entry.Description;
                entity.Content = entry.Content;
                entity.IsHot = entry.IsHot;
                entry.IsFeatured = entry.IsFeatured;
                entry.IsDisabled = entry.IsDisabled;

                entity.UpdatedDate = DateTime.Now;
                entity.UpdatedById = userId;

                // categories
                if (entry.CategoryIds != null && entity.Categories.Any())
                {
                    var categories = NewsCategoryRepository.GetByIds(entry.CategoryIds).ToList();

                    entity.Categories.Clear();
                    entity.Categories = categories;
                }

                // tags
                if (entry.TagIds != null && entity.Tags.Any())
                {
                    var tags = TagRepository.GetByIds(entry.TagIds).ToList();

                    entity.Tags.Clear();
                    entity.Tags = tags;
                }

                // upload image
                if (entry.Image != null)
                {
                    var imageName = GalleryHelper.UploadGallery(entry.Image);

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

            if (entry.CategoryIds == null || !entry.CategoryIds.Any())
            {
                violations.Add(new ErrorExtraInfo { Code = ErrorCodeType.NewsCategoryIsEmpty, Property = "CategoryIds" });
            }


            if (violations.Any())
                throw new ValidationError(violations);

        }

        private void ValidateUpdateEntryData(NewsUpdateEntry entry)
        {
            if (entry == null)
            {
                var violations = new List<ErrorExtraInfo>
                {
                    new ErrorExtraInfo {Code = ErrorCodeType.InvalidData}
                };
                throw new ValidationError(violations);
            }

            if (string.IsNullOrWhiteSpace(entry.Title))
            {
                var violations = new List<ErrorExtraInfo>
                {
                    new ErrorExtraInfo {Code = ErrorCodeType.InvalidName, Property = "Title"}
                };
                throw new ValidationError(violations);
            }

        }

        #endregion

        #region EntityMap
        private IEnumerable<NewsSummary> EntitiesToDtos(IEnumerable<News> entities)
        {
            return entities.Select(x => new NewsSummary
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Thumbnail = x.Image.Thumbnail,
                Views = x.Views,
                CreatedDate = x.CreatedDate,
                UpdatedDate = x.UpdatedDate,
                UpdatedBy = x.Editor.Firstname
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
                IsDisabled = entity.IsDisabled ?? false,
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
