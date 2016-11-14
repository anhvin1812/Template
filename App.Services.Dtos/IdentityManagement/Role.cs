using System.ComponentModel.DataAnnotations;
using App.Services.Dtos.Common;

namespace App.Services.Dtos.IdentityManagement
{
    public class RoleEntry : DtoBase
    {
        public int RoleId { get; set; }

        public int RoleName { get; set; }
    }

    public class RoleDetails : DtoBase
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public string Description { get; set; }
    }

    public class PermissionDetails : DtoBase
    {
        public int PermisstionId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public string Description { get; set; }
    }
}
