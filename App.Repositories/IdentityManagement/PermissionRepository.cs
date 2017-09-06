using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Permission;
using App.Core.Repositories;
using App.Entities.IdentityManagement;

namespace App.Repositories.IdentityManagement
{
    public class PermissionRepository : RepositoryBase, IPermissionRepository
    {
        public PermissionRepository(IMinhKhangDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        private IMinhKhangDatabaseContext DatabaseContext => PlatformContext as IMinhKhangDatabaseContext;

        public bool HasRoleClaim(int userId, string permissionCapability, ApplicationPermissions permission)
        {
            //return DatabaseContext.Get<User>().Where(u => u.Id == userId)
            //                            .Select(u=>u.UserRoles.Where(x=> x.RoleClaims.Any(c => c.ClaimType == permissionCapability 
            //                                            && (c.ClaimValue == permission.ToString() || c.ClaimValue == ApplicationPermissions.Super.ToString())
            //    ))).Any();
            DatabaseContext.MinhKhangDbContext.Database.Log = s => Debug.WriteLine(s);
            return DatabaseContext.Get<Role>().Any(t => t.Users.Any(x => x.UserId == userId) 
                                && t.RoleClaims.Any(c => c.ClaimType == permissionCapability 
                                            && (c.ClaimValue == permission.ToString() || c.ClaimValue == ApplicationPermissions.Super.ToString()))
                                );
        }

        #region Dispose
        private bool _disposed = false;

        protected override void Dispose(bool isDisposing)
        {
            if (!this._disposed)
            {
                if (isDisposing)
                {
                    
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion
    }
}
