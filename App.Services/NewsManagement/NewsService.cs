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

        public NewsService(IUnitOfWork unitOfWork, INewsRepository newsRepository)
            : base(unitOfWork, new IRepository[] { newsRepository }, new IService[] {  })
        {
            NewsRepository = newsRepository;
        }

        #endregion

        #region Public Methods

        public IEnumerable<NewsSummary> GetAll(string keyword, int? categoryId, int? page, int? pageSize, ref int? recordCount)
        {
            var news = NewsRepository.GetAll(keyword, categoryId, page, pageSize, ref recordCount)
                .Select(x => new NewsSummary
                {
                    Id = x.Id,
                    Title = x.Title,
                    Image = x.Image.Thumbnail,
                    Category = x.Category.Name,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate,
                    UpdatedBy = x.Editor.Firstname
                });
            
            return news;
        }

        public IEnumerable<NewsSummary> GetRelatedNews(int newsId, int categoryId, int? maxRecords = null)
        {
            var products = NewsRepository.GetRelatedNews(newsId, categoryId, maxRecords)
                .Select(x => new NewsSummary
                {
                    Id = x.Id,
                    Title = x.Title,
                    Image = x.Image.Thumbnail,
                    Category = x.Category.Name,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate,
                    UpdatedBy = x.Editor.Firstname
                });

            return products;
        }

        public NewsDetail GetById(int id)
        {
            var news = NewsRepository.GetById(id);

            if (news == null)
                throw new DataNotFoundException();

            return new NewsDetail
            {
                Id = news.Id,
                Title = news.Title,
                Content = news.Content,
                CategoryId = news.CategoryId,
                Category = news.Category.Name,
                CreatedDate = news.CreatedDate,
                UpdatedDate = news.UpdatedDate,
                UpdatedBy = news.Editor.Firstname
            };
        }

        public NewsUpdateEntry GetProductForEditing(int id)
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
                CategoryId = news.CategoryId,
                Thumbnail = news.Image.Thumbnail
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
                    CategoryId = entry.CategoryId,
                    CreatedDate = DateTime.Now,
                    UpdatedById = userId
                };

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
                entity.CategoryId = entry.CategoryId;
                entity.UpdatedDate = DateTime.Now;
                entity.UpdatedById = userId;
                
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

            NewsRepository.Update(entity);
            Save();
        }

        #endregion


        #region Private Methods

        private void ValidateEntryData(NewsEntry entry)
        {

            if (entry == null)
            {
                var violations = new List<ErrorExtraInfo>
                {
                    new ErrorExtraInfo {Code = ErrorCodeType.InvalidData}
                };
                throw new ValidationError(violations);
            }

            if ( string.IsNullOrWhiteSpace(entry.Title) )
            {
                var violations = new List<ErrorExtraInfo>
                {
                    new ErrorExtraInfo {Code = ErrorCodeType.InvalidName, Property = "Title"}
                };
                throw new ValidationError(violations);
            }
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


        #region Dispose
        private bool _disposed = false;

        protected override void Dispose(bool isDisposing)
        {
            if (!this._disposed)
            {
                if (isDisposing)
                {
                    NewsRepository = null;
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion

    }
}
