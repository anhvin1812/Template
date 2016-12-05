using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using App.Services.Dtos.Common;

namespace App.Services.Dtos.IdentityManagement
{
    public class UserEntry : DtoBase
    {
        public UserEntry()
        {
            Roles = new List<RoleSelection>();    
            SelectedRoles = new int[0];
        }


        [Required]
        [Display(Name = "First name")]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string Lastname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        [Display(Name = "Roles")]
        public int[] SelectedRoles { get; set; }
        public List<RoleSelection> Roles { get; set; }
    }
    public class UserSummary : DtoBase
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

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
