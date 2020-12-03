using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StartupProject_Asp.NetCore_PostGRE.Data.Enums
{
    public enum EPolicy
    {
        [Display(Name = "Role-Claim Policy")]
        [Description("Role-Claim-View")]
        RoleClaimView = 0,
        [Description("Role")]
        Role,
        [Description("Claim")]
        Claim
    }
}
