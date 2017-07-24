using System.Collections.Generic;
using App.Core.Repositories;
using App.Entities.NewsManagement;
using App.Entities.Settings;

namespace App.Repositories.Settings
{
    public interface ISettingRepository : IRepository
    {
        #region Settings

        Setting GetAllSettings();
        string GetMenu();
        void InsertSetting(Setting entity);
        void UpdateSetting(Setting entity);

        #endregion

        #region Homepage layout

        IEnumerable<HomepageLayout> GetAllHomepageLayout();
        HomepageLayout GetHomepageLayoutById(int id);
        void InsertHomepageLayout(HomepageLayout entity);
        void UpdateHomepageLayout(HomepageLayout entity);
        void DeleteHomepageLayout(int id);
        int GetHomepageLayoutMaxSortOrder();


        #endregion
    }
}
