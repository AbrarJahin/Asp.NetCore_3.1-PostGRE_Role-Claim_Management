using System.ComponentModel;

namespace StartupProject_Asp.NetCore_PostGRE.Data.Enums
{
    public enum EClaim
    {
        //[Display(Name = "Role-Claim Policy")]
        [Description("RoleClaim.View")]
        RoleClaimView = 0,
        [Description("Role.Create")]
        RoleCreate,
        [Description("Claim.Create")]
        ClaimCreate
    }
}
