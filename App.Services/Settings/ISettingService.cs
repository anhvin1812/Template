using System.Collections.Generic;
using App.Entities.Settings;
using App.Services.Dtos.NewsManagement;
using App.Services.Dtos.Settings;
using App.Services.Dtos.UI;

namespace App.Services.Settings
{
    public interface ISettingService : IService
    {
        #region Settings

        SettingDetail GetSetting();
        OptionDetail GetOptions();
        string GetMenu();
        void UpdateMenu(string menu);
        void UpdateOptions(OptionEntry entry);
        #endregion

        #region Homepage layout

        IEnumerable<HomepageLayOutDetail> GetAllHomepageLayout();
        void InsertHomepageLayout(HomepageLayoutEntry entry);
        void UpdateHomepageLayout(List<HomepageLayoutEntry> entries);
        void DeleteHomepageLayout(int id);

        #endregion

    }
}
