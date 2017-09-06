using System.Collections.Generic;
using System.Linq;
using App.Core.Repositories;
using App.Entities.IdentityManagement;
using App.Repositories.Common;

namespace App.Repositories.IdentityManagement
{
    public class RoleRepository : RepositoryBase, IRoleRepository
    {
        private IMinhKhangDatabaseContext DatabaseContext => PlatformContext as IMinhKhangDatabaseContext;


        public RoleRepository(IMinhKhangDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }


        public Role GetById(int id)
        {
            return DatabaseContext.FindById<Role>(id);
        }

        public Role GetByName(string roleName)
        {
            return DatabaseContext.Get<Role>().FirstOrDefault(t => t.Name == roleName);
        }

        public bool RoleExists(string roleName)
        {
            return DatabaseContext.Get<Role>().Any(t => t.Name == roleName);
        }

        public IEnumerable<Role> GetAll(int? page, int? pageSize, ref int? recordCount)
        {
            var result = DatabaseContext.Get<Role>();

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

        public IEnumerable<Role> GetByUserId(int userId)
        {
            return DatabaseContext.Get<Role>().Where(t => t.Users.Any(u => u.UserId == userId));
        }

        public IEnumerable<Role> GetByIds(IEnumerable<int> ids)
        {
            return DatabaseContext.Get<Role>().Where(t => ids.Contains(t.Id));
        }

        public void Insert(Role entity)
        {
            DatabaseContext.Insert(entity);
        }

        public void Update(Role entity)
        {
            DatabaseContext.Update(entity);
        }

        public void Delete(int id)
        {
            DatabaseContext.Delete<Role>(id);
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
