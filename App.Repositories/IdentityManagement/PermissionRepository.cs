using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Permission;
using App.Core.Repositories;
using App.Entities.IdentityManagement;
using App.Infrastructure.IdentityManagement;
using Microsoft.AspNet.Identity;
using User = App.Entities.IdentityManagement.User;

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
            return DatabaseContext.Get<User>().Where(u => u.Id == userId)
                                        .Select(u=>u.UserRoles.Where(x=>
                                            x.RoleClaims.Any(c => c.ClaimType == permissionCapability 
                                                        && (c.ClaimValue == permission.ToString() || c.ClaimValue == ApplicationPermissions.Super.ToString())
                ))).Any();
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
