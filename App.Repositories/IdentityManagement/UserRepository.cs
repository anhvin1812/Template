using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Repositories;
using App.Data.EntityFramework;
using App.Entities.IdentityManagement;
using App.Infrastructure.IdentityManagement;
using App.Repositories.Common;
using Microsoft.AspNet.Identity;
using User = App.Entities.IdentityManagement.User;

namespace App.Repositories.IdentityManagement
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        private ApplicationUserManager _applicationUserManager;
        private ApplicationRoleManager _applicationRoleManager;

        public UserRepository(IMinhKhangDatabaseContext databaseContext, ApplicationUserManager applicationUserManager)
            : base(databaseContext)
        {
            _applicationUserManager = applicationUserManager;
        }

        private IMinhKhangDatabaseContext DatabaseContext => PlatformContext as IMinhKhangDatabaseContext;

        public User GetUserById(int id)
        {
            var result = _applicationUserManager.FindById(id);

            return result;    
        }

        public IEnumerable<User> GetAllUser(int? page, int? pageSize, ref int? recordCount)
        {
            var db = new MinhKhangDbContext();

            var result = DatabaseContext.Get<User>();

            if (recordCount != null)
            {
                recordCount = result.Count();
            }

            if (page != null && pageSize != null)
            {
                result = result.ApplyPaging(page.Value, pageSize.Value);
            }

            return result;
        }

        public void Create(User user)
        {
            _applicationUserManager.Create(user);
        }

        public void Update(User user)
        {
            _applicationUserManager.Update(user);
        }

        public void Delete(User user)
        {
            _applicationUserManager.Delete(user);
        }

        public IEnumerable<Role> GetRoleByUser(int useId)
        {
            return DatabaseContext.Get<Role>().Where(r => r.Users.Any(u => u.UserId == useId));
        }


        #region Dispose
        private bool _disposed = false;

        protected override void Dispose(bool isDisposing)
        {
            if (!this._disposed)
            {
                if (isDisposing)
                {
                    _applicationUserManager = null;

                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion
    }
}
