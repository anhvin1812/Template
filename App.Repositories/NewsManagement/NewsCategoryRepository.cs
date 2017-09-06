using System.Collections.Generic;
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

        public IEnumerable<NewsCategory> GetAll(bool? isDisabled, int? page, int? pageSize, ref int? recordCount)
        {
            var result = DatabaseContext.Get<NewsCategory>();

            if(isDisabled != null)
            {
                result = (isDisabled == false) ? result.Where(t => t.IsDisabled != true) : result.Where(t => t.IsDisabled == true);
            }

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

        public IEnumerable<NewsCategory> GetByIds(IEnumerable<int> ids)
        {
            return DatabaseContext.Get<NewsCategory>().Where(t => ids.Contains(t.Id));
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

        public bool IsExistedName(string name, int? id = null)
        {
            var result = DatabaseContext.Get<NewsCategory>().Any(t => t.Name == name);

            if (id.HasValue)
                result = DatabaseContext.Get<NewsCategory>().Any(t => t.Id != id);

            return result;
        }
    }
}
