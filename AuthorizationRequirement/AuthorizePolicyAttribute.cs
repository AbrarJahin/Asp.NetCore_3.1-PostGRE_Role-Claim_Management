using Microsoft.AspNetCore.Authorization;
using StartupProject_Asp.NetCore_PostGRE.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StartupProject_Asp.NetCore_PostGRE.AuthorizationRequirement
{
    public class AuthorizePolicyAttribute : AuthorizeAttribute
    {
        public AuthorizePolicyAttribute(params EClaim[] allowedPolicies)
        {
            //Max 1 policy is applicable
            IEnumerable<string> allowedPoliciesAsStrings = allowedPolicies.Select(x => Enum.GetName(typeof(EClaim), x));
            Policy = string.Join("", allowedPoliciesAsStrings);
        }
    }
}
