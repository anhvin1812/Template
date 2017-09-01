using System.Collections.Generic;
using System.Linq;
using App.Core.Repositories;
using App.Entities.NewsManagement;
using App.Repositories.Common;

namespace App.Repositories.NewsManagement
{
    public class TagRepository : RepositoryBase, ITagRepository
    {
        private IMinhKhangDatabaseContext DatabaseContext => PlatformContext as IMinhKhangDatabaseContext;


        public TagRepository(IMinhKhangDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        public IEnumerable<Tag> GetAll(string keyword, bool? isDisabled, int? page, int? pageSize, ref int? recordCount)
        {
            var result = DatabaseContext.Get<Tag>();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                result = result.Where(t => t.Name.Contains(keyword));
            }

            if (isDisabled.HasValue)
            {
                result = isDisabled == true ? result.Where(t => t.IsDisabled == true) : result.Where(t=>t.IsDisabled != true);
            }

            if (recordCount != null)
            {
                recordCount = result.Count();
            }

            if (page != null && pageSize != null)
            {
                result = result.OrderBy(t=>t.Name).ApplyPaging(page.Value, pageSize.Value);
            }

            return result;
        }

        public IEnumerable<Tag> GetMostUsedTags(bool? isDisabled = null, int maxRecords = 10)
        {
            var result = DatabaseContext.Get<Tag>();

            if (isDisabled.HasValue)
            {
                result = isDisabled == true ? result.Where(t => t.IsDisabled == true) : result.Where(t => t.IsDisabled != true);
            }

            result = result.OrderByDescending(t => t.Newses.Count).Take(maxRecords);

            return result;
        }

        public IEnumerable<Tag> GetByIds(IEnumerable<int> ids)
        {
            return DatabaseContext.Get<Tag>().Where(t => ids.Contains(t.Id));
        }

        public Tag GetById(int id)
        {
            return DatabaseContext.Get<Tag>().FirstOrDefault(t=>t.Id == id);
        }

        public Tag GetByName(string name)
        {
            return DatabaseContext.Get<Tag>().FirstOrDefault(t => t.Name == name);
        }

        public void Insert(Tag entity)
        {
            DatabaseContext.Insert(entity);
        }

        public void Update(Tag entity)
        {
            DatabaseContext.Update(entity);
        }

        public void Delete(int id)
        {
            DatabaseContext.Delete<Tag>(id);
        }
    }
}
