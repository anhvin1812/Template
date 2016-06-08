using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace App.Entities.IdentityManagement
{
    public class Permission : EntityBase
    {
        public int Id { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
