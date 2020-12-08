using System.ComponentModel.DataAnnotations;

namespace StartupProject_Asp.NetCore_PostGRE.Data.Enums
{
    public enum ELeaveType
    {
        [Display(Name = "Annual Leave")]
        Annual = 0,
        [Display(Name = "Home Leave")]
        Home,
        [Display(Name = "Special Leave")]
        Special,
        [Display(Name = "Maternity Leave")]
        Maternity,
        [Display(Name = "Sick Leave")]
        Sick
    }
}
