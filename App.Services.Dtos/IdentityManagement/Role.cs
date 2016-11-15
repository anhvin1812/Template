using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using App.Core.Permission;
using App.Services.Dtos.Common;

namespace App.Services.Dtos.IdentityManagement
{
    public class RoleEntry : DtoBase
    {
        public string RoleName { get; set; }

        public string Description { get; set; }

        public List<PermissionEntry> Permissions { get; set; }
    }

    public class RoleSummary : DtoBase
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public string Description { get; set; }
    }

    public class RoleDetails : DtoBase
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public string Description { get; set; }

        public List<PermissionSummary> Permissions { get; set; }
    }

    public class PermissionSummary : DtoBase
    {
        public int PermisstionId { get; set; }
        public string ClaimType { get; set; }
        public ApplicationPermissions ClaimValue { get; set; }
    }

    public class PermissionEntry : DtoBase
    {
        public string ClaimType { get; set; }
        public ApplicationPermissions ClaimValue { get; set; }
        public string Description { get; set; }
    }
}
