using System.Collections.Generic;
using System.Linq;
using App.Core.Repositories;
using App.Entities.NewsManagement;
using App.Entities.Settings;
using App.Repositories.Common;

namespace App.Repositories.Settings
{
    public class SettingRepository : RepositoryBase, ISettingRepository
    {
        private IMinhKhangDatabaseContext DatabaseContext => PlatformContext as IMinhKhangDatabaseContext;


        public SettingRepository(IMinhKhangDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        #region Settings

        public Setting GetAllSettings()
        {
            return DatabaseContext.Get<Setting>().FirstOrDefault();
        }

        public string GetMenu()
        {
            return DatabaseContext.Get<Setting>().Select(t=>t.Menu).FirstOrDefault();
        }

        public void InsertSetting(Setting entity)
        {
            DatabaseContext.Insert(entity);
        }

        public void UpdateSetting(Setting entity)
        {
            DatabaseContext.Update(entity);
        }

        #endregion

        #region Homepage layout

        public IEnumerable<HomepageLayout> GetAllHomepageLayout()
        {
            return DatabaseContext.Get<HomepageLayout>();
        }

        public HomepageLayout GetHomepageLayoutById(int id)
        {
            return DatabaseContext.FindById<HomepageLayout>(id);
        }

        public void InsertHomepageLayout(HomepageLayout entity)
        {
            DatabaseContext.Insert(entity);
        }

        public void UpdateHomepageLayout(HomepageLayout entity)
        {
            DatabaseContext.Update(entity);
        }

        public void DeleteHomepageLayout(int id)
        {
            DatabaseContext.Delete<HomepageLayout>(id);
        }

        public int GetHomepageLayoutMaxSortOrder()
        {
            return DatabaseContext.Get<HomepageLayout>().Max(t => t.SortOrder) ?? 0;
        }

        #endregion
    }
}
