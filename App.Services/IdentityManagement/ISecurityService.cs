using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core.Permission;
using App.Services.Dtos.IdentityManagement;

namespace App.Services.IdentityManagement
{
    public interface ISecurityService : IService
    {
        bool HasPermission(int userId, string permissionCapability, ApplicationPermissions permission);

    }
}
