using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Permission;
using App.Core.Repositories;
using App.Repositories.IdentityManagement;
using App.Services.Dtos.IdentityManagement;

namespace App.Services.IdentityManagement
{
    public class PermissionService: ServiceBase, IPermissionService
    {
        #region Contructor
        //private IUserRepository UserRepository { get; set; }

        public PermissionService(IUnitOfWork unitOfWork)
            : base(unitOfWork, new IRepository[] {}, new IService[] { })
        {
        }

        #endregion

        #region Public Methods

        public IEnumerable<RoleClaimSummary> GetAllPermissions()
        {
            var permissions = new[]
            {
                new RoleClaimSummary {ClaimType = ApplicationPermissionCapabilities.USER, ClaimValue = ApplicationPermissions.Create.ToString()}, 
                new RoleClaimSummary {ClaimType = ApplicationPermissionCapabilities.USER, ClaimValue = ApplicationPermissions.Read.ToString()}, 
                new RoleClaimSummary {ClaimType = ApplicationPermissionCapabilities.USER, ClaimValue = ApplicationPermissions.Modify.ToString()}, 
                new RoleClaimSummary {ClaimType = ApplicationPermissionCapabilities.USER, ClaimValue = ApplicationPermissions.Delete.ToString()}, 
                new RoleClaimSummary {ClaimType = ApplicationPermissionCapabilities.USER, ClaimValue = ApplicationPermissions.Super.ToString()},

                new RoleClaimSummary {ClaimType = ApplicationPermissionCapabilities.NEWS, ClaimValue = ApplicationPermissions.Create.ToString()},
                new RoleClaimSummary {ClaimType = ApplicationPermissionCapabilities.NEWS, ClaimValue = ApplicationPermissions.Read.ToString()},
                new RoleClaimSummary {ClaimType = ApplicationPermissionCapabilities.NEWS, ClaimValue = ApplicationPermissions.Modify.ToString()},
                new RoleClaimSummary {ClaimType = ApplicationPermissionCapabilities.NEWS, ClaimValue = ApplicationPermissions.Delete.ToString()},
                new RoleClaimSummary {ClaimType = ApplicationPermissionCapabilities.NEWS, ClaimValue = ApplicationPermissions.Super.ToString()},

                new RoleClaimSummary {ClaimType = ApplicationPermissionCapabilities.PRODUCT, ClaimValue = ApplicationPermissions.Create.ToString()},
                new RoleClaimSummary {ClaimType = ApplicationPermissionCapabilities.PRODUCT, ClaimValue = ApplicationPermissions.Read.ToString()},
                new RoleClaimSummary {ClaimType = ApplicationPermissionCapabilities.PRODUCT, ClaimValue = ApplicationPermissions.Modify.ToString()},
                new RoleClaimSummary {ClaimType = ApplicationPermissionCapabilities.PRODUCT, ClaimValue = ApplicationPermissions.Delete.ToString()},
                new RoleClaimSummary {ClaimType = ApplicationPermissionCapabilities.PRODUCT, ClaimValue = ApplicationPermissions.Super.ToString()},

                new RoleClaimSummary {ClaimType = ApplicationPermissionCapabilities.ORDER, ClaimValue = ApplicationPermissions.Create.ToString()},
                new RoleClaimSummary {ClaimType = ApplicationPermissionCapabilities.ORDER, ClaimValue = ApplicationPermissions.Read.ToString()},
                new RoleClaimSummary {ClaimType = ApplicationPermissionCapabilities.ORDER, ClaimValue = ApplicationPermissions.Modify.ToString()},
                new RoleClaimSummary {ClaimType = ApplicationPermissionCapabilities.ORDER, ClaimValue = ApplicationPermissions.Delete.ToString()},
                new RoleClaimSummary {ClaimType = ApplicationPermissionCapabilities.ORDER, ClaimValue = ApplicationPermissions.Super.ToString()},
            };
            
            return permissions;
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
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion

    }
}
