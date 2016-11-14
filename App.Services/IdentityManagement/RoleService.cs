using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Repositories;
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

        public IEnumerable<RoleDetails> GetAll(int? page, int? pageSize, ref int? recordCount)
        {
            var users = RoleRepository.GetAll(page, pageSize, ref recordCount)
                .Select(x => new RoleDetails
                {
                    RoleId = x.Id,
                    RoleName = x.Name,
                    Description = x.Description
                });
            
            return users;
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
