using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Permission;
using App.Core.Repositories;
using App.Entities.ProductManagement;

namespace App.Repositories.IdentityManagement
{
    public interface IPermissionRepository : IRepository
    {
        bool HasRoleClaim(int userId, string permissionCapability, ApplicationPermissions permission);
    }
}
