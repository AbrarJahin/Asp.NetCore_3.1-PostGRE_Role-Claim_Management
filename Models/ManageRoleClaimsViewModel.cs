using System;

namespace StartupProject_Asp.NetCore_PostGRE.Models
{
    public class ManageRoleClaimsViewModel
    {
        public int Id { get; set; }
        public Guid RoleId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public bool Selected { get; set; }
    }
}
