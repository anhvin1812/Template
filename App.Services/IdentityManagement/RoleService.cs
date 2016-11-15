using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Permission;
using App.Core.Repositories;
using App.Entities;
using App.Entities.IdentityManagement;
using App.Repositories.IdentityManagement;
using App.Services.Dtos.IdentityManagement;

namespace App.Services.IdentityManagement
{
    public class RoleService : ServiceBase, IRoleService
    {
        #region Contructor
        private IRoleRepository RoleRepository { get; set; }

        public RoleService(IUnitOfWork unitOfWork, IRoleRepository roleRepository)
            : base(unitOfWork, new IRepository[] { roleRepository }, new IService[] { })
        {
            RoleRepository = roleRepository;
        }

        #endregion

        #region Public Methods

        public IEnumerable<RoleSummary> GetAll(int? page, int? pageSize, ref int? recordCount)
        {
            var roles = RoleRepository.GetAll(page, pageSize, ref recordCount)
                .Select(x => new RoleSummary
                {
                    RoleId = x.Id,
                    RoleName = x.Name,
                    Description = x.Description
                });
            
            return roles;
        }

        public RoleEntry GetBlankRoleEntry()
        {
            return new RoleEntry
            {
                RoleClaims = GetAllPermissions()
            };
        }

        public RoleDetails GetById(int id)
        {
            var roleEntity = RoleRepository.GetById(id);

            //TODO: implemetn exception
            if(roleEntity == null)
                throw new Exception();
            
            var result = new RoleDetails
            {
                RoleId = roleEntity.Id,
                RoleName = roleEntity.Name,
                Description = roleEntity.Description,
                RoleClaims = roleEntity.RoleClaims.Select(x => new RoleClaimSummary
                {
                    RoleClaimId = x.Id,
                    RoleId = x.RoleId,
                    ClaimType = x.ClaimType,
                    ClaimValue = x.ClaimValue
                }).ToList()
            };

            return result;
        }

        public void Insert(RoleEntry entry)
        {
            //TODO: Check exit Role, permission,...

            var roleEntity = new Role
            {
                Name = entry.RoleName,
                Description = entry.Description,
                RoleClaims = entry.RoleClaims.Select(x=>new RoleClaim
                {
                    ClaimType = x.ClaimType,
                    ClaimValue = x.ClaimValue,
                }).ToList()
            };

            RoleRepository.Insert(roleEntity);
            Save();
        }

        public void Update(int id, RoleEntry entry)
        {
            //TODO: Check exit Role, permission,...

            var roleEntity = RoleRepository.GetById(id);
            //TODO: throw exception
            if (roleEntity == null)
                throw new Exception();

            roleEntity.Name = entry.RoleName;
            roleEntity.Description = entry.Description;
            roleEntity.RoleClaims = entry.RoleClaims.Select(x => new RoleClaim
            {
                ClaimType = x.ClaimType,
                ClaimValue = x.ClaimValue,
            }).ToList();

            RoleRepository.Update(roleEntity);
            Save();
        }


        #endregion

        #region Private Methods
        private List<RoleClaimSummary> GetAllPermissions()
        {
            var permissions = new List<RoleClaimSummary>
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
                    RoleRepository = null;
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion

    }
}
