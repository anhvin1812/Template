using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Repositories;
using App.Entities.IdentityManagement;
using App.Infrastructure.IdentityManagement;
using App.Repositories.Common;
using Microsoft.AspNet.Identity;
using User = App.Entities.IdentityManagement.User;

namespace App.Repositories.IdentityManagement
{
    public class RoleRepository : RepositoryBase, IRoleRepository
    {
        private ApplicationRoleManager _applicationRoleManager;

        public RoleRepository(IMinhKhangDatabaseContext databaseContext, ApplicationRoleManager applicationRoleManager)
            : base(databaseContext)
        {
            _applicationRoleManager = applicationRoleManager;
        }

        private IMinhKhangDatabaseContext DatabaseContext => DatabaseContext as IMinhKhangDatabaseContext;

        public Role GetRoleById(int id)
        {
            var result = _applicationRoleManager.FindById(id);

            return result;    
        }

        public IEnumerable<Role> GetAllRole(int? page, int? pageSize, ref int? recordCount)
        {
            var result = _applicationRoleManager.Roles;

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

        public void Create(Role role)
        {
            _applicationRoleManager.Create(role);
        }

        public void Update(Role role)
        {
            _applicationRoleManager.Update(role);
        }

        public void Delete(Role role)
        {
            _applicationRoleManager.Delete(role);
        }



        #region Dispose
        private bool _disposed = false;

        protected override void Dispose(bool isDisposing)
        {
            if (!this._disposed)
            {
                if (isDisposing)
                {
                    _applicationRoleManager = null;

                }
                _disposed = true;
            }
            base.Dispose(isDisposing);
        }
        #endregion


    }
}
