﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using App.Core.Repositories;
using App.Entities.NewsManagement;
using App.Entities.ProductManagement;
using App.Repositories.Common;
using App.Services.Dtos.NewsManagement;

namespace App.Repositories.NewsManagement
{
    public class NewsRepository : RepositoryBase, INewsRepository
    {
        private IMinhKhangDatabaseContext DatabaseContext => PlatformContext as IMinhKhangDatabaseContext;


        public NewsRepository(IMinhKhangDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        public IEnumerable<News> GetAll(string keyword, int? categoryId, int? statusId, int? mediaTypeId, bool? hot, bool? featured, int? page, int? pageSize, ref int? recordCount)
        {
            var result = DatabaseContext.Get<News>();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                result = result.Where(t => t.Title.Contains(keyword) || t.Description.Contains(keyword));
            }

            //get by category
            if (categoryId.HasValue)
            {
                result = result.Where(t => t.Categories.Any(c=>c.Id == categoryId));
            }
            
            //get by status
            if (statusId.HasValue)
            {
                result = result.Where(t => t.StatusId == statusId);
            }

            //get by media type
            if (mediaTypeId.HasValue)
            {
                result = result.Where(t => t.MediaTypeId == mediaTypeId);
            }

            //get hot news
            if (hot.HasValue)
            {
                result = result.Where(t => t.IsHot == hot.Value);
            }

            //get featured news
            if (featured.HasValue)
            {
                result = result.Where(t => t.IsFeatured == featured.Value);
            }

            if (recordCount != null)
            {
                recordCount = result.Count();
            }

            result = result.OrderByDescending(t => t.Id);

            if (page != null && pageSize != null)
            {
                result = result.ApplyPaging(page.Value, pageSize.Value);
            }

            return result;
        }

        public News GetById(int id)
        {
            return DatabaseContext.FindById<News>(id);
        }

        public void Insert(News entity)
        {
            DatabaseContext.Insert(entity);
        }

        public void Update(News entity)
        {
            DatabaseContext.Update(entity);
        }

        public void Delete(int id)
        {
            DatabaseContext.Delete<News>(id);
        }

        public bool IsExist(string title)
        {
            return DatabaseContext.Get<News>().Any(t => t.Title == title);
        }
        #region Status

        public IEnumerable<NewsStatus> GetAllStatus()
        {
            return DatabaseContext.Get<NewsStatus>();
        }

        public NewsStatus GetStatusById(int id)
        {
            return DatabaseContext.FindById<NewsStatus>(id);
        }

        #endregion


        #region Public news
        public IEnumerable<News> GetFeaturedNews(int? maxRecords = 5)
        {
            var result = GetPublicNews().Where(t => t.IsFeatured == true);

            if (maxRecords.HasValue)
            {
                result = result.OrderByDescending(t => t.CreatedDate).Take(maxRecords.Value);
            }

            return result;
        }

        public IEnumerable<News> GetHotNews(int? maxRecords = 10)
        {
            var result = GetPublicNews().Where(t => t.IsHot == true);

            if (maxRecords.HasValue)
            {
                result = result.OrderByDescending(t => t.CreatedDate).Take(maxRecords.Value);
            }

            return result;
        }

        public IEnumerable<News> GetLatestNews(int categoryId, int? maxRecords = 5)
        {
            var result = GetPublicNews().Where(t => t.Categories.Any(c => c.Id == categoryId));

            if (maxRecords.HasValue)
            {
                result = result.OrderByDescending(t => t.CreatedDate).Take(maxRecords.Value);
            }

            return result;
        }

        public IEnumerable<News> GetRelatedNews(int newsId, int categoryId, int? maxRecords = 5)
        {
            var result = GetPublicNews().Where(t => t.Id < newsId);

            result = result.Where(t => t.Categories.Any(c => c.Id == categoryId));

            if (maxRecords.HasValue)
            {
                result = result.OrderByDescending(t=>t.CreatedDate).Take(maxRecords.Value);
            }

            return result;
        }
        public News GetPublicNewsById(int id)
        {
            return GetPublicNews().FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<News> GetMostViews(DateTime? startDate = null, DateTime? endDate = null, int? maxRecords = 5)
        {
            var result = GetPublicNews();

            if (startDate.HasValue)
                result = result.Where(t=>t.CreatedDate >= startDate.Value);

            if (endDate.HasValue)
                result = result.Where(t => DbFunctions.TruncateTime(t.CreatedDate) <= endDate.Value);

            if (maxRecords.HasValue)
            {
                result = result.OrderByDescending(t=>t.Views).Take(maxRecords.Value);
            }

            return result;
        }

        public IEnumerable<News> GetAllPublicNews(string keyword, DateTime? startDate, DateTime? endDate, int? categoryId, int? tagId, int? page, int? pageSize, ref int? recordCount)
        {
            var result = GetPublicNews();

            //get by keyword
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                result = result.Where(t => t.Title.Contains(keyword) || t.Description.Contains(keyword));
            }

            //get by date
            if (startDate.HasValue)
                result = result.Where(t => t.CreatedDate >= startDate.Value);

            if (endDate.HasValue)
                result = result.Where(t => DbFunctions.TruncateTime(t.CreatedDate) <= endDate.Value);

            //get by tagId
            if (tagId.HasValue)
            {
                result = result.Where(t => t.Tags.Any(c => c.Id == tagId.Value));
            }

            //get by categoryId
            if (categoryId.HasValue)
            {
                result = result.Where(t => t.Categories.Any(c => c.Id == categoryId.Value));
            }

            if (recordCount != null)
            {
                recordCount = result.Count();
            }

            if (page != null && pageSize != null)
            {
                result = result.OrderByDescending(t => t.CreatedDate).ApplyPaging(page.Value, pageSize.Value);
            }

            return result;
        }

        private IQueryable<News> GetPublicNews()
        {
            return DatabaseContext.Get<News>().Where(t => t.StatusId == (int) Core.News.NewsStatus.Public);
        }
        #endregion
    }
}
