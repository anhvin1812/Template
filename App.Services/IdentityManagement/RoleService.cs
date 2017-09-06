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
using App.Services.Common;
using App.Services.Dtos.IdentityManagement;
using App.Services.Dtos.UI;

namespace App.Services.IdentityManagement
{
    public class RoleService : ServiceBase, IRoleService
    {
        #region Contructor
        private ISecurityService SecurityService { get; set; }
        private IRoleRepository RoleRepository { get; set; }

        public RoleService(IUnitOfWork unitOfWork, ISecurityService securityService, IRoleRepository roleRepository)
            : base(unitOfWork, new IRepository[] { roleRepository }, new IService[] { securityService })
        {
            RoleRepository = roleRepository;
            SecurityService = securityService;
        }

        #endregion

        #region Public Methods

        public IEnumerable<RoleSummary> GetAll(int? page, int? pageSize, ref int? recordCount)
        {
            var userId = CurrentClaimsIdentity.GetUserId();
            if (!SecurityService.HasPermission(userId, ApplicationPermissionCapabilities.USER,ApplicationPermissions.Read))
                throw new PermissionException();

            var roles = RoleRepository.GetAll(page, pageSize, ref recordCount)
                .Select(x => new RoleSummary
                {
                    RoleId = x.Id,
                    RoleName = x.Name,
                    Description = x.Description
                });
            
            return roles;
        }

        public SelectListOptions GetOptionsForDropdownList()
        {
            int? recordCount = null;
            var roles = RoleRepository.GetAll(null, null, ref recordCount);

            return new SelectListOptions {
                Items = roles.Select(x => new OptionItem
                {
                    Value = x.Id,
                    Text = x.Name
                })
            };
            
        }

        public RoleEntry GetBlankRoleEntry()
        {
            return new RoleEntry
            {
                RoleClaims = GetAllPermissions()
            };
        }

        public RoleEntry GetRoleForEditing(int id)
        {
            var userId = CurrentClaimsIdentity.GetUserId();
            if (!SecurityService.HasPermission(userId, ApplicationPermissionCapabilities.USER, ApplicationPermissions.Modify))
                throw new PermissionException();

            var roleEntity = RoleRepository.GetById(id);

            if(roleEntity == null)
                throw new DataNotFoundException();

            var allPermissions = GetAllPermissions();
            // set checked for current claims
            foreach (var roleClaimn in roleEntity.RoleClaims)
            {
                var permission = allPermissions.FirstOrDefault(x => x.ClaimType == roleClaimn.ClaimType && x.ClaimValue == roleClaimn.ClaimValue);
                if (permission != null)
                    permission.IsChecked = true;
            }

            var result = new RoleEntry
            {
                RoleName = roleEntity.Name,
                Description = roleEntity.Description,
                RoleClaims = allPermissions
            };

            return result;
        }

        public void Insert(RoleEntry entry)
        {
            var userId = CurrentClaimsIdentity.GetUserId();
            if (!SecurityService.HasPermission(userId, ApplicationPermissionCapabilities.USER, ApplicationPermissions.Create))
                throw new PermissionException();

            // Validate data
            ValidateEntryData(entry);

            // Check existed name
            var isExisted = RoleRepository.RoleExists(entry.RoleName);
            if (isExisted)
            {
                var violations = new List<ErrorExtraInfo>
                {
                    new ErrorExtraInfo {Code = ErrorCodeType.RoleNameIsUsed, Property = "RoleName"}
                };
                throw new ValidationError(violations);
            }

            var roleClaims = entry.RoleClaims.Where(x=>x.IsChecked);

            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required))
            {
                // Save role
                var roleEntity = new Role
                {
                    Name = entry.RoleName,
                    Description = entry.Description
                };
                RoleRepository.Insert(roleEntity);
                Save();

                // Add Claim into role
                if (roleClaims != null && roleClaims.Any())
                {
                    foreach (var roleClaim in roleClaims)
                    {
                        RoleRepository.InsertRoleClaim(new RoleClaim
                        {
                            RoleId = roleEntity.Id,
                            ClaimType = roleClaim.ClaimType,
                            ClaimValue = roleClaim.ClaimValue
                        });
                    }

                    Save();
                }

                transactionScope.Complete();
            }
          
        }

        public void Update(int id, RoleEntry entry)
        {
            var userId = CurrentClaimsIdentity.GetUserId();
            if (!SecurityService.HasPermission(userId, ApplicationPermissionCapabilities.USER, ApplicationPermissions.Modify))
                throw new PermissionException();

            // Validate data
            ValidateEntryData(entry);

            var roleEntity = RoleRepository.GetById(id);

            // Validate entity
            ValidateEntityData(roleEntity);

            // Check existed name
            var roleForValidation = RoleRepository.GetByName(entry.RoleName);
            if (roleForValidation != null && roleForValidation.Id != roleEntity.Id)
            {
                var violations = new List<ErrorExtraInfo>
                {
                    new ErrorExtraInfo {Code = ErrorCodeType.RoleNameIsUsed, Property = "RoleName"}
                };
                throw new ValidationError(violations);
            }

            var roleClaims = entry.RoleClaims.Where(x => x.IsChecked);
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required))
            {
                // Remmove old claims
                var oldClaims = RoleRepository.GetRoleClaimsByRoleId(roleEntity.Id);
                foreach (var claim in oldClaims)
                {
                    RoleRepository.DeleteRoleClaim(claim.Id);
                }

                // Add new claims into role
                if (roleClaims != null && roleClaims.Any())
                {
                    foreach (var roleClaim in roleClaims)
                    {
                        RoleRepository.InsertRoleClaim(new RoleClaim
                        {
                            RoleId = roleEntity.Id,
                            ClaimType = roleClaim.ClaimType,
                            ClaimValue = roleClaim.ClaimValue
                        });
                    }
                }
                // Update role
                roleEntity.Name = entry.RoleName;
                roleEntity.Description = entry.Description;

                RoleRepository.Update(roleEntity);
                Save();

                transactionScope.Complete();
            }
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

        private bool IsChecked(RoleClaim roleClaim, List<RoleClaimSummary> allPermissions)
        {
            return allPermissions.Any(x => x.ClaimType == roleClaim.ClaimType && x.ClaimValue == roleClaim.ClaimValue);
        }

        private void ValidateEntryData(RoleEntry entry)
        {
            var violations = new List<ErrorExtraInfo>();

            if (entry == null)
            {
                violations.Add(new ErrorExtraInfo { Code = ErrorCodeType.InvalidData });
                throw new ValidationError(violations);
            }

            if ( string.IsNullOrWhiteSpace(entry.RoleName) )
            {
                violations.Add(new ErrorExtraInfo {Code = ErrorCodeType.InvalidRoleName, Property = "RoleName"});
            }

            if(violations.Any())
                throw new ValidationError(violations);
        }

        private void ValidateEntityData(Role entity)
        {
            if (entity == null)
            {
                var violations = new List<ErrorExtraInfo>
                {
                    new ErrorExtraInfo {Code = ErrorCodeType.RoleIsNotExisted}
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
                    SecurityService = null;
                    RoleRepository = null;
                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion

    }
}
