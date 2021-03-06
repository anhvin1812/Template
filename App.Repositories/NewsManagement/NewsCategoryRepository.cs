﻿using System.Collections.Generic;
using System.Linq;
using App.Core.Repositories;
using App.Entities.NewsManagement;
using App.Repositories.Common;

namespace App.Repositories.NewsManagement
{
    public class NewsCategoryRepository : RepositoryBase, INewsCategoryRepository
    {
        private IMinhKhangDatabaseContext DatabaseContext => PlatformContext as IMinhKhangDatabaseContext;


        public NewsCategoryRepository(IMinhKhangDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        public IEnumerable<NewsCategory> GetAll(int? page, int? pageSize, ref int? recordCount)
        {
            var result = DatabaseContext.Get<NewsCategory>();

            if (recordCount != null)
            {
                recordCount = result.Count();
            }

            if (page != null && pageSize != null)
            {
                result = result.OrderBy(t=>t.Id).ApplyPaging(page.Value, pageSize.Value);
            }

            return result;
        }

        public IEnumerable<NewsCategory> GetByParentId(int? parentId)
        {
            var result = DatabaseContext.Get<NewsCategory>().Where(t=>t.ParentId == parentId);

            return result;
        }

        public NewsCategory GetById(int id)
        {
            return DatabaseContext.Get<NewsCategory>().FirstOrDefault(t=>t.Id == id);
        }

        public void Insert(NewsCategory entity)
        {
            DatabaseContext.Insert(entity);
        }

        public void Update(NewsCategory entity)
        {
            DatabaseContext.Update(entity);
        }

        public void Delete(int id)
        {
            DatabaseContext.Delete<NewsCategory>(id);
        }
    }
}
