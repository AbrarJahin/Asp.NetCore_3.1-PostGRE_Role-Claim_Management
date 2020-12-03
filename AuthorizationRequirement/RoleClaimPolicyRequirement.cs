using Microsoft.AspNetCore.Authorization;
using StartupProject_Asp.NetCore_PostGRE.Data;
using StartupProject_Asp.NetCore_PostGRE.Data.Enums;

namespace StartupProject_Asp.NetCore_PostGRE.AuthorizationRequirement
{
    internal class RoleClaimPolicyRequirement : IAuthorizationRequirement
    {
        //https://stackoverflow.com/questions/39590060/how-to-access-dbcontext-session-in-custom-policy-based-authorization
        //https://damienbod.com/2017/10/23/implementing-custom-policies-in-asp-net-core-using-the-httpcontext/
        private readonly ApplicationDbContext _dbContext;
        private EClaim _policyType;

        public RoleClaimPolicyRequirement(ApplicationDbContext context, EClaim policy)
        {
            _policyType = policy;
            _dbContext = context;
        }

        /*protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        MyRequirement requirement)
        {
            // Do something with _context
            // Check if the requirement is fulfilled.
            return Task.CompletedTask;
        }*/

        /*protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        RoleClaimRequirement requirement)
        {
            // Do something with _context
            // Check if the requirement is fulfilled.
            return Task.CompletedTask;
        }*/
    }
}