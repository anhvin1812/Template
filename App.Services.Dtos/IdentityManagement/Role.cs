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

        public List<RoleClaimSummary> RoleClaims { get; set; }
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

        public List<RoleClaimSummary> RoleClaims { get; set; }
    }

    public class RoleClaimSummary : DtoBase
    {
        public int RoleClaimId { get; set; }
        public int RoleId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public bool IsChecked { get; set; }
    }

    public class RoleClaimEditEntry : DtoBase
    {
        public int RoleId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        
    }

}
