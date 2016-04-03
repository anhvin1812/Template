using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace App.Entities.IdentityManagement
{
    public class UserRole : IdentityUserRole<int>, IObjectState
    {
        [NotMapped]
        public ObjectState State { get; set; }
    }
}
