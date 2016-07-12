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
    }
}
