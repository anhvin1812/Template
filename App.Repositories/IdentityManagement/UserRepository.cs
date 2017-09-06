using System.Collections.Generic;
using System.Linq;
using App.Core.Repositories;
using App.Entities;
using App.Entities.IdentityManagement;
using App.Infrastructure.IdentityManagement;
using App.Repositories.Common;
using Microsoft.AspNet.Identity;

namespace App.Repositories.IdentityManagement
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        private ApplicationUserManager _applicationUserManager;
        private ApplicationRoleManager _applicationRoleManager;

        public UserRepository(IMinhKhangDatabaseContext databaseContext)
            : base(databaseContext)
        {
            _applicationUserManager = ApplicationUserManager.Create(databaseContext.MinhKhangDbContext);
        }

        private IMinhKhangDatabaseContext DatabaseContext => PlatformContext as IMinhKhangDatabaseContext;

        public User GetUserById(int id)
        {
            var result = DatabaseContext.FindById<User>(id);
            return result;    
        }

        public IEnumerable<User> GetAll(string term, bool? lockoutEnabled, bool? emailConfirmed, int? page, int? pageSize, ref int? recordCount)
        {
            var result = DatabaseContext.Get<User>();

            if (!string.IsNullOrWhiteSpace(term))
            {
                result = result.Where(t => (t.Lastname + " " + t.Firstname).Contains(term)
                || t.Email.Contains(term) || t.PhoneNumber.Contains(term));
            }

            if (lockoutEnabled != null)
            {
                result = result.Where(t => t.LockoutEnabled == lockoutEnabled);
            }

            if (emailConfirmed != null)
            {
                result = result.Where(t => t.EmailConfirmed == emailConfirmed);
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

        public void Create(User user, string password)
        {
            user.PasswordHash = _applicationUserManager.PasswordHasher.HashPassword(password);
            DatabaseContext.Insert(user);
        }

        public void Update(User user)
        {
            //_applicationUserManager.Update(user);
            DatabaseContext.Update(user);
        }

        public void Delete(User user)
        {
            _applicationUserManager.Delete(user);
        }

        public IEnumerable<Role> GetRoleByUser(int useId)
        {
            return DatabaseContext.Get<Role>().Where(r => r.Users.Any(u => u.UserId == useId));
        }

        public bool IsExistedEmail(string email, int? id = null)
        {
            var result = DatabaseContext.Get<User>().Any(t => t.Email == email);

            if (id.HasValue)
                result = DatabaseContext.Get<User>().Any(t => t.Email == email && t.Id != id);

            return result;
        }

        #region UserRole
        public void DeleteRoles(IEnumerable<UserRole> entities)
        {
            foreach (var entity in entities)
            {
                entity.State = ObjectState.Deleted; 
            }
        }

        public void InsertUserRole(UserRole entity)
        {
            DatabaseContext.Insert(entity);
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
                    _applicationUserManager = null;

                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion
    }
}
