using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace App.Entities.IdentityManagement
{
    public class RoleClaim : EntityBase
    {
        public int Id { get; set; }
        public int RoleId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        public virtual Role Role { get; set; }
    }
}
