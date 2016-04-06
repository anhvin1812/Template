using System.ComponentModel.DataAnnotations;
using App.Services.Dtos.Common;

namespace App.Services.Dtos.IdentityManagement
{
    public class LogOnModel : DtoBase
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
}
