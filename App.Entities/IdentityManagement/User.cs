using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace App.Entities.ProductManagement
{
    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>, IObjectState
    {
        public string Firstname  { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Role> UserRoles { get; set; }

        [NotMapped]
        public ObjectState State { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
