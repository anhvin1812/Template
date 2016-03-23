using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace App.Infastructure.IdentityManagement
{
    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>
    {
    }
}
