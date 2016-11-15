using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Services.Dtos.IdentityManagement;

namespace App.Services.IdentityManagement
{
    public interface IRoleService : IService
    {
        IEnumerable<RoleSummary> GetAll(int? page, int? pageSize, ref int? recordCount);

        RoleDetails GetById(int id);

        void Insert(RoleEntry entry);

        void Update(int id, RoleEntry entry);


    }
}
