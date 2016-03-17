using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities.IdentityManagement
{
    public class UserRole : EntityBase
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
