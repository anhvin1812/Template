﻿using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace App.Entities.IdentityManagement
{
    public class CustomRole : IdentityRole<int, UserRole>
    {

        [NotMapped]
        public ObjectState State { get; set; }
    }
}
