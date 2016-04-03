﻿using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace App.Entities.IdentityManagement
{
    public class UserLogin : IdentityUserLogin<int>
    {
        [NotMapped]
        public ObjectState State { get; set; }
    }
}