using Microsoft.AspNetCore.Authorization;
using StartupProject_Asp.NetCore_PostGRE.Data.Enums;

namespace StartupProject_Asp.NetCore_PostGRE.AuthorizationRequirement
{
    internal class ClaimsRoleRequirement : IAuthorizationRequirement
    {
        public readonly EClaim eClaimValue;

        public ClaimsRoleRequirement(object claimValue)
        {
            eClaimValue = (EClaim)claimValue;
            //name = eClaimValue.ToString();
            //description = eClaimValue.Description();
        }
    }
}