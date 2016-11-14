using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Repositories;
using App.Data.EntityFramework;
using App.Entities.IdentityManagement;
using App.Infrastructure.IdentityManagement;
using App.Repositories.Common;
using Microsoft.AspNet.Identity;
using User = App.Entities.IdentityManagement.User;

namespace App.Repositories.IdentityManagement
{
    public class RoleRepository : RepositoryBase, IRoleRepository
    {

        public RoleRepository(IMinhKhangDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }


        public Role GetById(int id)
        {
            return PlatformContext.Get<Role>().FirstOrDefault(r=>r.Id == id);
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
                result = result.ApplyPaging(page.Value, pageSize.Value);
            }

            return result;
        }

        public void Create(Role role)
        {
            PlatformContext.Insert(role);
        }

        public void Update(Role role)
        {
            PlatformContext.Update(role);
        }

        public void Delete(Role role)
        {
            PlatformContext.Delete<Role>(role);
        }
       
    }
}
