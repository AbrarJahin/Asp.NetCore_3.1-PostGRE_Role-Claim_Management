using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StartupProject_Asp.NetCore_PostGRE.Data;
using StartupProject_Asp.NetCore_PostGRE.Data.Enums;
using StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace StartupProject_Asp.NetCore_PostGRE.AuthorizationRequirement
{
    internal class ClaimsRoleRequirementHandler : AuthorizationHandler<ClaimsRoleRequirement>,
    IAuthorizationRequirement
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public ClaimsRoleRequirementHandler(ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimsRoleRequirement requirement)
        {
            User user = await _userManager.GetUserAsync(context.User);
            if (user == null || context.User.Identity.IsAuthenticated == false)
            {
                context.Fail();
                return;
            }
            string userId = user.Id.ToString();
            EClaim claim = requirement.eClaimValue;

            var roleClaims = await _dbContext.RoleClaims
                                //.Where(rc => rc.ClaimType == claim.ToString() && rc.ClaimValue == claim.Description())
                                .Where(rc => rc.ClaimType == claim.ToString())
                                .Select(r=> r.RoleId)
                                .Distinct()
                                .ToListAsync();
            var userRole = await _dbContext.UserRoles
                                .Where(ur=> ur.UserId == user.Id)
                                .Select(r => r.RoleId)
                                .Distinct()
                                .ToListAsync();
            bool hasSameElements = roleClaims.Intersect(userRole).Any();

            if (hasSameElements)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
                return;
            }
        }
    }
}
