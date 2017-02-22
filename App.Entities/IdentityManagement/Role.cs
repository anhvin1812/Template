using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace App.Entities.ProductManagement
{
    [Serializable]
    public class Role : IdentityRole<int, UserRole>, IObjectState
    {
        public string Description { get; set; }
        public virtual ICollection<User> RoleUsers { get; set; }
        public virtual ICollection<RoleClaim> RoleClaims { get; set; }


        [NotMapped]
        public ObjectState State { get; set; }
    }
}
