using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace App.Entities.IdentityManagement
{
    [Serializable]
    public class Role : IdentityRole<int, UserRole>, IObjectState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class.
        /// </summary>
        public Role() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Role(string name) { Name = name; }

        [NotMapped]
        public ObjectState State { get; set; }
    }
}
