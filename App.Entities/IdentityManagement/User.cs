using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using App.Entities.FileManagement;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace App.Entities.IdentityManagement
{
    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>, IObjectState
    {
        public string Firstname  { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public byte? Gender { get; set; }
        public int? GalleryId { get; set; }

        public virtual ICollection<Role> UserRoles { get; set; }

        [ForeignKey("GalleryId")]
        public virtual Gallery ProfilePicture { get; set; }

        [NotMapped]
        public ObjectState State { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            
            // Add custom user claims here
            userIdentity.AddClaim(new Claim(ClaimTypes.Email, this.Email));
            userIdentity.AddClaim(new Claim(ClaimTypes.GivenName, $"{this.Lastname} {this.Firstname}"));
            userIdentity.AddClaim(new Claim(ClaimTypes.Thumbprint, this.ProfilePicture?.Thumbnail));
            userIdentity.AddClaim(new Claim(ClaimTypes.Role, manager.GetRoles(this.Id).FirstOrDefault() ));
            
            return userIdentity;
        }
    }
}
