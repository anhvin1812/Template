using System;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNet.Identity;

namespace App.Infrastructure.IdentityManagement
{
    public static class IdentityExtension
    {
        public static string GetFullName(this IIdentity identity)
        {
            var myIdentity = identity as ClaimsIdentity;

            if (identity == null)
                return null;

            return myIdentity.FindFirstValue(ClaimTypes.GivenName);
        }

        public static string GetUserEmail(this IIdentity identity)
        {
            var myIdentity = identity as ClaimsIdentity;

            if (identity == null)
                return null;
           
            return myIdentity.FindFirstValue(ClaimTypes.Email);
        }

        public static string GetProfileThumnail(this IIdentity identity)
        {
            var myIdentity = identity as ClaimsIdentity;

            if (identity == null)
                return null;

            return myIdentity.FindFirstValue(ClaimTypes.Thumbprint);
        }

        public static string GetRole(this IIdentity identity)
        {
            var myIdentity = identity as ClaimsIdentity;

            if (identity == null)
                return null;

            return myIdentity.FindFirstValue(ClaimTypes.Role);
        }
    }
}
