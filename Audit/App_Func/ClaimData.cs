﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace Audit
{
    public static class GenericPrincipalExtensions
    {
        public static string GetClaimData(this IPrincipal user, string claimName)
        {
            if (user.Identity.IsAuthenticated)
            {
                ClaimsIdentity claimsIdentity = user.Identity as ClaimsIdentity;
                foreach (var claim in claimsIdentity.Claims)
                {
                    if (claim.Type == claimName)
                        return claim.Value;
                }
                return "";
            }
            else
                return "";
        }
    }
}