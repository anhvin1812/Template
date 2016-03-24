using Microsoft.AspNet.Identity.EntityFramework;

namespace App.Core.Identity
{
    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>
    {
    }
}
