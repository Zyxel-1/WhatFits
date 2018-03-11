﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Security.Claims;


namespace Whatfits.UserAccessControl.Auth
{
    public class AuthorizationManager : ClaimsAuthorizationManager
    {
        public override bool CheckAccess(AuthorizationContext context)
        {
            // get claim type/value from AuthorizationContext
            string type = context.Resource.First().Value;
            string value = context.Action.First().Value;

            // get principal from AuthorizationContext
            var principal = context.Principal;

            // Check if Has Claim
            if(principal.HasClaim(type, value))
            {
                return true; // return true if has claim
            }
            
            return false;
        }
    }
}