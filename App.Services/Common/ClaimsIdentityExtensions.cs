using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using App.Core.Common;
using Microsoft.AspNet.Identity;

namespace App.Services.Common
{
    public static class ClaimsIdentityExtensions
    {
        //public static int GetNetworkId(this ClaimsPrincipal identity)
        //{
        //    var networkClaim = identity.Claims.FirstOrDefault(c => c.Type == Core.IdentityManagement.SherpaClaims.NetworkId);
        //    if (networkClaim == null)
        //    {
        //        throw new KeyNotFoundException();
        //    }
        //    return networkClaim.Value.ToInt();
        //}

        public static int GetUserId(this ClaimsPrincipal identity)
        {
            var userId = identity.Identity.GetUserId();
            if (userId == null)
            {
                throw new KeyNotFoundException();
            }
            return userId.ToInt();
        }

        //public static int? GetDefaultGroupId(this ClaimsPrincipal identity)
        //{
        //    var defaultFroupId = identity.Claims.FirstOrDefault
        //        (c => c.Type == Core.IdentityManagement.SherpaClaims.MemberDefaultGroupId);

        //    if (defaultFroupId == null)
        //    {
        //        return null;
        //    }
        //    return defaultFroupId.Value.ToInt();
        //}

        //public static int? GetRoleId(this ClaimsPrincipal identity)
        //{
        //    var memberIdClaim = identity.Claims.FirstOrDefault(c => c.Type == Core.IdentityManagement.SherpaClaims.RoleId);
        //    if (memberIdClaim == null)
        //    {
        //        return null;
        //    }
        //    return memberIdClaim.Value.ToInt();
        //}

        //public static int? GetCanDeleteNews(this ClaimsPrincipal identity)
        //{
        //    var defaultFroupId = identity.Claims.FirstOrDefault
        //        (c => c.Type == Core.IdentityManagement.SherpaClaims.CanDeleteNews);

        //    if (defaultFroupId == null)
        //    {
        //        return null;
        //    }
        //    return defaultFroupId.Value.ToInt();
        //}

        //public static int? GetCanEditNews(this ClaimsPrincipal identity)
        //{
        //    var defaultFroupId = identity.Claims.FirstOrDefault
        //        (c => c.Type == Core.IdentityManagement.SherpaClaims.CanEditNews);

        //    if (defaultFroupId == null)
        //    {
        //        return null;
        //    }
        //    return defaultFroupId.Value.ToInt();
        //}

        //public static string GetBaseUrl(this ClaimsPrincipal identity)
        //{
        //    var defaultBaseUrl = identity.Claims.FirstOrDefault
        //        (c => c.Type == NetworkProperties.PROPNAME_BASEURL);

        //    return defaultBaseUrl == null ? null : defaultBaseUrl.Value;
        //}

        ////public static bool IsSystemAdmin(this ClaimsPrincipal identity)
        ////{
        ////    var authMethodClaim = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.AuthenticationMethod);
        ////    var result = authMethodClaim != null && authMethodClaim.Value == Core.IdentityManagement.AuthenticationSchemes.Token;
        ////    return result;
        ////}

        ////public static bool IsNetworkAdmin(this ClaimsPrincipal identity)
        ////{
        ////    // ToDo: Vish, please implement this
        ////    var authMethodClaim = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.AuthenticationMethod);
        ////    var result = authMethodClaim != null && authMethodClaim.Value == Core.IdentityManagement.AuthenticationSchemes.Token;
        ////    return result;
        ////}

        ////public static bool IsRoamingUser(this ClaimsPrincipal identity)
        ////{
        ////    // ToDo: Vish, please implement this
        ////    var authMethodClaim = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.AuthenticationMethod);
        ////    var result = authMethodClaim != null && authMethodClaim.Value == Core.IdentityManagement.AuthenticationSchemes.Token;
        ////    return result;
        ////}

        //public static bool IsAdmin(this ClaimsPrincipal identity)
        //{
        //    return IsSystemAdmin(identity) || IsNetworkAdmin(identity) || (identity.HasClaim(SherpaClaims.IsAdmin, SherpaClaims.True));
        //}

        ////public static bool IsAdmin(this ClaimsPrincipal identity)
        ////{
        ////    var authMethodClaim = identity.Claims.FirstOrDefault(c => c.Type == SherpaClaims.IsAdmin);
        ////    var result = authMethodClaim != null && authMethodClaim.Value == SherpaClaims.True;
        ////    return result;
        ////}

        //public static bool IsSystemAdmin(this ClaimsPrincipal identity)
        //{
        //    var authMethodClaim = identity.Claims.FirstOrDefault(c => c.Type == SherpaClaims.IsSystemAdmin);
        //    var result = authMethodClaim != null && authMethodClaim.Value == SherpaClaims.True;
        //    return result;
        //}

        //public static bool IsNetworkAdmin(this ClaimsPrincipal identity)
        //{
        //    var authMethodClaim = identity.Claims.FirstOrDefault(c => c.Type == SherpaClaims.IsNetworkAdmin);
        //    var result = authMethodClaim != null && authMethodClaim.Value == SherpaClaims.True;
        //    return result;
        //}

        //public static bool IsRoamingUser(this ClaimsPrincipal identity)
        //{
        //    var authMethodClaim = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.AuthenticationMethod);
        //    var result = authMethodClaim != null && authMethodClaim.Value == Core.IdentityManagement.AuthenticationSchemes.Token;
        //    return result;
        //}



        //public static bool IsGroupAdmin(this ClaimsPrincipal identity, int networkGroupId)
        //{
        //    var authMethodClaim = identity.Claims.FirstOrDefault(c => c.Type == SherpaClaims.GroupsAdmin);
        //    if (authMethodClaim != null && !authMethodClaim.Value.IsNullOrEmpty())
        //    {
        //        var groupIds = authMethodClaim.Value.Split(',');
        //        return groupIds.Contains(networkGroupId.ToString());
        //    }
        //    return false;
        //}

        //public static bool IsGroupAdmin(this ClaimsPrincipal identity)
        //{
        //    var authMethodClaim = identity.Claims.FirstOrDefault(c => c.Type == SherpaClaims.GroupsAdmin);
        //    if (authMethodClaim != null && !authMethodClaim.Value.IsNullOrEmpty())
        //    {
        //        return true;
        //    }
        //    return false;
        //}

    }
}
