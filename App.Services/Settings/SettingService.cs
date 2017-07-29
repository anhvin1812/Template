
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.Core.Exceptions;
using App.Core.News;
using App.Core.Repositories;
using App.Entities.NewsManagement;
using App.Entities.ProductManagement;
using App.Entities.Settings;
using App.Infrastructure.File;
using App.Repositories.NewsManagement;
using App.Repositories.ProductManagement;
using App.Repositories.Settings;
using App.Services.Dtos.NewsManagement;
using App.Services.Dtos.ProductManagement;
using App.Services.Dtos.Settings;
using App.Services.Dtos.UI;

namespace App.Services.Settings
{
    public class SettingService : ServiceBase, ISettingService
    {
        #region Contructor
        private ISettingRepository SettingRepository { get; set; }

        public SettingService(IUnitOfWork unitOfWork, ISettingRepository settingRepository)
            : base(unitOfWork, new IRepository[] { settingRepository }, new IService[] { })
        {
            SettingRepository = settingRepository;
        }

        #endregion

        #region Public Methods

        #region Settings

        public SettingDetail GetSetting()
        {
            var result = SettingRepository.GetAllSettings();
            if(result != null)
            {
                return new SettingDetail
                {
                    Menu = result.Menu,
                    Facebook = result.Facebook,
                    Skype = result.Skype,
                    Email = result.Email,
                    PhoneNumber = result.PhoneNumber,
                    Logo = result.Logo,
                    Website = result.Website,
                    Address = result.Address
                };
            }

            return new SettingDetail();
        }

        public OptionDetail GetOptions()
        {
            var result = SettingRepository.GetAllSettings();
            if (result != null)
            {
                return new OptionDetail
                {
                    Facebook = result.Facebook,
                    Skype = result.Skype,
                    Email = result.Email,
                    PhoneNumber = result.PhoneNumber,
                    Logo = result.Logo,
                    Website = result.Website,
                    Address = result.Address
                };
            }

            return new OptionDetail();
        }

        public string GetMenu()
        {
            var result = SettingRepository.GetMenu();
            return result;
        }

        public void UpdateMenu(string menu)
        {
            var entity = SettingRepository.GetAllSettings();
            if (entity != null)
            {
                entity.Menu = menu;
                SettingRepository.UpdateSetting(entity);
            }
            else
            {
                entity = new Setting
                {
                    Menu = menu
                };
                SettingRepository.InsertSetting(entity);
            }

            Save();
        }

        public void UpdateOptions(OptionEntry entry)
        {
            var entity = SettingRepository.GetAllSettings();
            if (entity != null)
            {
                entity.Address = entry.Address;
                entity.PhoneNumber = entry.PhoneNumber;
                entity.Email = entry.Email;
                entity.Skype = entry.Skype;
                entity.Website = entry.Website;
                entity.Facebook = entry.Facebook;
            }
            else
            {
                entity = new Setting
                {
                    Address = entry.Address,
                    PhoneNumber = entry.PhoneNumber,
                    Email = entry.Email,
                    Skype = entry.Skype,
                    Website = entry.Website,
                    Facebook = entry.Facebook
                };
                SettingRepository.InsertSetting(entity);
            }

            // logo
            if(entry.Logo != null)
            {
                var imageName = GalleryHelper.UploadLogo(entry.Logo);
                entity.Logo = imageName;
            }

            SettingRepository.UpdateSetting(entity);

            Save();
        }
        #endregion

        #region Homepage layout

        public IEnumerable<HomepageLayOutDetail> GetAllHomepageLayout()
        {
            var results = SettingRepository.GetAllHomepageLayout();
            return results.Select(x => new HomepageLayOutDetail
            {
                Id = x.Id,
                CategoryId = x.CategoryId,
                Title = x.Category?.Name,
                MediaType = (MediaType?) x.MediaTypeId,
                LayoutType = (LayoutType?) x.MediaTypeId,
                SortOrder = x.SortOrder
            });
        }

        public void InsertHomepageLayout(HomepageLayoutEntry entry)
        {
            ValidateHomepageLayoutEntry(entry);

            var sortOrder = SettingRepository.GetHomepageLayoutMaxSortOrder();

            var entity = new HomepageLayout
            {
                CategoryId = entry.CategoryId,
                MediaTypeId = (int?)entry.MediaType,
                LayoutTypeId = (int?)entry.LayoutType,
                SortOrder = sortOrder + 1
            };

            SettingRepository.InsertHomepageLayout(entity);
            Save();
        }

        public void UpdateHomepageLayout(List<HomepageLayoutEntry> entries)
        {
            var allLayouts = SettingRepository.GetAllHomepageLayout();

            foreach (var entry in entries)
            {
                var entity = allLayouts.FirstOrDefault( x => x.Id == entry.Id);

                if (entity != null)
                {
                    entity.CategoryId = entry.CategoryId;
                    entity.MediaTypeId = (int?)entry.MediaType;
                    entity.LayoutTypeId = (int?)entry.LayoutType;
                    entity.SortOrder = entry.SortOrder;

                    SettingRepository.UpdateHomepageLayout(entity);
                }
                else
                {
                    entity = new HomepageLayout
                    {
                        CategoryId = entry.CategoryId,
                        MediaTypeId = (int?)entry.MediaType,
                        LayoutTypeId = (int?)entry.LayoutType,
                        SortOrder = entry.SortOrder,
                    };

                    SettingRepository.InsertHomepageLayout(entity);
                }
            }

            // Delete unused layouts
            var usingLayoutIds = entries.Where(x => x.Id != null).Select(x => x.Id);
            var deleteLayouts = allLayouts.Where(x => !usingLayoutIds.Contains(x.Id));
            foreach (var layout in deleteLayouts)
            {
                SettingRepository.DeleteHomepageLayout(layout.Id);
            }

            Save();
        }

        public void DeleteHomepageLayout(int id)
        {
            var entity = SettingRepository.GetHomepageLayoutById(id);

            if (entity == null)
                throw new DataNotFoundException();

            SettingRepository.DeleteHomepageLayout(id);
            Save();
        }

        #endregion

        #endregion

        #region Private Methods
        private void ValidateHomepageLayoutEntry(HomepageLayoutEntry entry)
        {
            if (entry == null)
            {
                var violations = new List<ErrorExtraInfo>
                {
                    new ErrorExtraInfo {Code = ErrorCodeType.InvalidData}
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
                    SettingRepository = null;
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion

    }
}
