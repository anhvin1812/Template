using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using App.Core.Exceptions;
using App.Core.Repositories;
using App.Entities;
using App.Entities.NewsManagement;
using App.Infrastructure.File;
using App.Repositories.NewsManagement;
using App.Services.Common;
using App.Services.Dtos.NewsManagement;
using App.Services.Dtos.UI;
using App.Services.Gallery;

namespace App.Services.NewsManagement
{
    public class PublicNewsService : ServiceBase, IPublicNewsService
    {
        #region Contructor
        private INewsRepository NewsRepository { get; set; }

        public PublicNewsService(IUnitOfWork unitOfWork, INewsRepository newsRepository)
            : base(unitOfWork, new IRepository[] { newsRepository }, 
                  new IService[] {  })
        {
            NewsRepository = newsRepository;
        }

        #endregion

        #region Public Methods
        public IEnumerable<PublicNewsSummary> GetAll(string keyword, DateTime? startDate, DateTime? endDate, int? categoryId, int? page, int? pageSize, ref int? recordCount)
        {
            var results = NewsRepository.GetAllPublicNews(keyword, startDate, endDate, categoryId, page, pageSize, ref recordCount);

            return EntitiesToDtos(results);
        }

        public PublicNewsDetail GetPublicNewsById(int id)
        {
            var result = NewsRepository.GetPublicNewsById(id);

            if (result == null)
                throw new DataNotFoundException();

            result.Views++;
            NewsRepository.Update(result);
            Save();

            return EntityToDto(result);
        }

        public IEnumerable<PublicNewsSummary> GetFeaturedNews(int? maxRecords = 5)
        {
            var results = NewsRepository.GetFeaturedNews(maxRecords);

            return EntitiesToDtos(results);
        }

        public IEnumerable<PublicNewsSummary> GetHotNews(int? maxRecords = 10)
        {
            var results = NewsRepository.GetHotNews(maxRecords);

            return EntitiesToDtos(results);
        }

        public IEnumerable<PublicNewsSummary> GetLatestNews(int categoryId, int? maxRecords = 5)
        {
            var results = NewsRepository.GetLatestNews(categoryId, maxRecords);

            return EntitiesToDtos(results);
        }

        public IEnumerable<PublicNewsSummary> GetRelatedNews(int newsId, int categoryId, int? maxRecords = 5)
        {
            var results = NewsRepository.GetRelatedNews(newsId, categoryId, maxRecords);

            return EntitiesToDtos(results);
        }

        public IEnumerable<PublicNewsSummary> GetMostViews(DateTime? startDate = null, DateTime? endDate = null, int? maxRecords = 5)
        {
            var results = NewsRepository.GetMostViews(startDate, endDate, maxRecords);

            return EntitiesToDtos(results);
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
        private IEnumerable<PublicNewsSummary> EntitiesToDtos(IEnumerable<News> entities)
        {
            return entities.Select(x => new PublicNewsSummary
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Views = x.Views,
                PublishedDate = x.CreatedDate,
                Thumbnail = x.Image.Thumbnail,
                UpdatedBy = $"{x.Editor.Firstname} {x.Editor.Lastname}",
                Category = x.Categories.Select(c=> new PublicCategorySummary { Id = c.Id, Name = c.Name}).LastOrDefault()
            });
        }

        private PublicNewsDetail EntityToDto(News entity)
        {
            return new PublicNewsDetail
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Thumbnail = entity.Image.Thumbnail,
                Image = entity.Image.Image,
                Content = entity.Content,
                PublishedDate = entity.CreatedDate,
                MediaTypeId = entity.MediaTypeId,
                Views = entity.Views,
                CreatedBy = $"{entity.Editor.Firstname} {entity.Editor.Lastname}",
                CreatedById = entity.Editor.Id,
                Tags = entity.Tags.Select(x=> new PublicTagSummary { Id = x.Id, Name =x.Name}).ToList(),
                Categories = entity.Categories.Select(x => new PublicCategorySummary { Id = x.Id, Name = x.Name }).ToList()
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
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion

    }
}
