using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace App.Entities.IdentityManagement
{
    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>
    {
        [NotMapped]
        public ObjectState State { get; set; }
    }
}
