using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Repositories;
using App.Data.EntityFramework;
using App.Entities.ProductManagement;
using App.Infrastructure.IdentityManagement;
using App.Repositories.Common;
using Microsoft.AspNet.Identity;
using User = App.Entities.ProductManagement.User;

namespace App.Repositories.IdentityManagement
{
    public class RoleRepository : RepositoryBase, IRoleRepository
    {
        private ApplicationRoleManager _applicationRoleManager;
        private IMinhKhangDatabaseContext DatabaseContext => PlatformContext as IMinhKhangDatabaseContext;


        public RoleRepository(IMinhKhangDatabaseContext databaseContext, ApplicationRoleManager applicationRoleManager)
            : base(databaseContext)
        {
            _applicationRoleManager = applicationRoleManager;
        }


        public Role GetById(int id)
        {
            return _applicationRoleManager.FindById(id);
        }

        public Role GetByName(string roleName)
        {
            return _applicationRoleManager.FindByName(roleName);
        }



        public bool RoleExists(string roleName)
        {
            return _applicationRoleManager.RoleExists(roleName);
        }

        public IEnumerable<Role> GetAll(int? page, int? pageSize, ref int? recordCount)
        {
            var result = PlatformContext.Get<Role>();

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

        public void Insert(Role entity)
        {
            _applicationRoleManager.Create(entity);
        }

        public void Update(Role entity)
        {
            _applicationRoleManager.Update(entity);
        }

        public void Delete(Role entity)
        {
            _applicationRoleManager.Delete(entity);
        }

        #region Role Claim
        public IEnumerable<RoleClaim> GetRoleClaimsByRoleId(int id)
        {
            return DatabaseContext.Get<RoleClaim>().Where(x=>x.RoleId == id);
        }

        public void InsertRoleClaim(RoleClaim entity)
        {
            DatabaseContext.Insert(entity);
        }

        public void DeleteRoleClaim(int id)
        {
            DatabaseContext.Delete<RoleClaim>(id);
        }
        #endregion


    }
}
