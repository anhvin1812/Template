using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace App.Services
{
    public interface IService : IDisposable
    {
        void SetIdentity(ClaimsPrincipal identity);
        void SetClaim(String claim, string value);
        void SetClaim(Claim claim);
    }
}
