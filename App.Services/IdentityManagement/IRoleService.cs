using System.Collections.Generic;
using App.Services.Dtos.IdentityManagement;
using App.Services.Dtos.UI;

namespace App.Services.IdentityManagement
{
    public interface IRoleService : IService
    {
        IEnumerable<RoleSummary> GetAll(int? page, int? pageSize, ref int? recordCount);
        SelectListOptions GetOptionsForDropdownList();

        RoleEntry GetBlankRoleEntry();

        RoleEntry GetRoleForEditing(int id);

        void Insert(RoleEntry entry);

        void Update(int id, RoleEntry entry);


    }
}
