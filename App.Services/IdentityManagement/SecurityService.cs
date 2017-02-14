using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using App.Core.Exceptions;
using App.Core.Permission;
using App.Core.Repositories;
using App.Entities;
using App.Entities.IdentityManagement;
using App.Repositories.IdentityManagement;
using App.Services.Dtos.IdentityManagement;

namespace App.Services.IdentityManagement
{
    public class SecurityService : ServiceBase, ISecurityService
    {
        #region Contructor
        private IPermissionRepository PermissionRepository { get; set; }

        public SecurityService(IUnitOfWork unitOfWork, IPermissionRepository permissionRepository)
            : base(unitOfWork, new IRepository[] { permissionRepository }, new IService[] { })
        {
            PermissionRepository = permissionRepository;
        }

        #endregion

        #region Public Methods

        public bool HasPermission(int userId, string permissionCapability, ApplicationPermissions permission)
        {
            return PermissionRepository.HasRoleClaim(userId, permissionCapability, permission);
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
                    PermissionRepository = null;
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion

    }
}
