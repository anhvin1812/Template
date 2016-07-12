using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace App.Entities.IdentityManagement
{
    [Serializable]
    public class Role : IdentityRole<int, UserRole>, IObjectState
    {
        public string Description { get; set; }

        [NotMapped]
        public ObjectState State { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
