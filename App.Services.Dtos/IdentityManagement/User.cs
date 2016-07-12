using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using App.Services.Dtos.Common;

namespace App.Services.Dtos.IdentityManagement
{
    public class UserEntry : DtoBase
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
    public class UserSummary : DtoBase
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public IEnumerable<RoleDetails> Roles { get; set; }
    }

    public class UserDetails : DtoBase
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

    }
}
