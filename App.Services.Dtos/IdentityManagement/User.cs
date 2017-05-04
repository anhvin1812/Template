using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using App.Services.Dtos.Common;
using App.Services.Dtos.Validations;

namespace App.Services.Dtos.IdentityManagement
{
    public class UserEntry : DtoBase
    {
        public UserEntry()
        {
            Roles = new List<int>();    
        }

        public int Id { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        [Email(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public string Address { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public bool LockoutEnabled { get; set; }

        [Display(Name = "Roles")]
        public List<int> Roles { get; set; }
    }
    public class UserSummary : DtoBase
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public bool LockoutEnabled { get; set; }

        public IEnumerable<RoleDetails> Roles { get; set; }
    }

    public class UserDetail : DtoBase
    {
        public UserDetail()
        {
            Roles = new List<RoleSummary>();
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public string Address { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public bool LockoutEnabled { get; set; }

        public List<RoleSummary> Roles { get; set; }
    }

}
