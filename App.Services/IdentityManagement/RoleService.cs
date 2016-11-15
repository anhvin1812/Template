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
                Permissions = roleEntity.Permissions.Select(x => new PermissionSummary
                {
                    PermisstionId = x.Id,
                    ClaimType = x.ClaimType,
                    ClaimValue = (ApplicationPermissions)Enum.Parse(typeof(ApplicationPermissions), x.ClaimValue)
                }).ToList()
            };

            return result;
        }

        public void Insert(RoleEntry entry)
        {
            //TODO: Check exit Role, permission,...

            var roleEntity = new Role
            {
                State = ObjectState.Added,
                Name = entry.RoleName,
                Description = entry.Description,
                Permissions = entry.Permissions.Select(x=>new Permission
                {
                    State = ObjectState.Added,
                    ClaimType = x.ClaimType,
                    ClaimValue = x.ClaimValue.ToString(),
                    Description = x.Description
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
            roleEntity.Permissions = entry.Permissions.Select(x => new Permission
            {
                State = ObjectState.Added,
                ClaimType = x.ClaimType,
                ClaimValue = x.ClaimValue.ToString(),
                Description = x.Description
            }).ToList();

            RoleRepository.Update(roleEntity);
            Save();
        }


        #endregion

        #region Private Methods

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
