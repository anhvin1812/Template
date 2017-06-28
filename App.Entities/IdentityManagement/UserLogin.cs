using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace App.Entities.ProductManagement
{
    public class UserLogin : IdentityUserLogin<int>, IObjectState
    {
        [NotMapped]
        public ObjectState State { get; set; }
    }
}
