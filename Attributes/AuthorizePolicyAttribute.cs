using Microsoft.AspNetCore.Authorization;
using StartupProject_Asp.NetCore_PostGRE.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StartupProject_Asp.NetCore_PostGRE.Attributes
{
    public class AuthorizePolicyAttribute : AuthorizeAttribute
    {
        public AuthorizePolicyAttribute(params EClaim[] allowedPolicies)
        {
            IEnumerable<string> allowedPoliciesAsStrings = allowedPolicies.Select(x => Enum.GetName(typeof(EClaim), x));
            //Policy = string.Join(",", allowedPoliciesAsStrings);
            Policy = string.Join("", allowedPoliciesAsStrings);
        }
    }
}
