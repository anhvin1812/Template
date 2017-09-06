using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using App.Core.User;
using App.Services.Dtos.Common;
using App.Services.Dtos.Validations;

namespace App.Services.Dtos.IdentityManagement
{
    public class UserEntry : DtoBase
    {
        public UserEntry()
        {
            RoleIds = new List<int>();    
        }

        public int Id { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public string Address { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please select gender")]
        public byte? Gender { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public bool LockoutEnabled { get; set; }

        public HttpPostedFileBase ProfilePicture { get; set; }

        public string Thumbnail { get; set; }

        public List<int> RoleIds { get; set; }
    }
    public class UserSummary : DtoBase
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public Gender? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public bool LockoutEnabled { get; set; }

        public IEnumerable<RoleSummary> Roles { get; set; }
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

        public DateTime? DateOfBirth { get; set; }

        public Gender? Gender { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public bool LockoutEnabled { get; set; }

        public string ProfilePicture { get; set; }

        public List<RoleSummary> Roles { get; set; }
    }


    public class UserFilter
    {
        public string Term { get; set; }
        public int? RoleId { get; set; }
        public bool? LockoutEnabled { get; set; }
        public bool? EmailConfirmed { get; set; }
    }

}
