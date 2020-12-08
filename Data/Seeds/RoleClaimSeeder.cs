using Microsoft.EntityFrameworkCore;
using StartupProject_Asp.NetCore_PostGRE.Data.Enums;
using StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity;
using System;
using System.Collections.Generic;

namespace StartupProject_Asp.NetCore_PostGRE.Data.Seeds
{
    public class RoleClaimSeeder
    {
        internal static void Execute(ModelBuilder builder, ICollection<Guid> superAdminRoleIdList)
        {
            int itemCount = -1;
            IList<RoleClaim> roleClaimList = new List<RoleClaim>();
            //IList<RoleClaim> roleClaimList = new List<RoleClaim>();
            foreach (Guid superAdminId in superAdminRoleIdList)
            {
                foreach (EClaim claim in Enum.GetValues(typeof(EClaim)))
                {
                    roleClaimList.Add(new RoleClaim
                    {
                        Id = itemCount,
                        RoleId = superAdminId,
                        ClaimType = claim.ToString(),
                        ClaimValue = claim.Description()
                    });
                    itemCount -= 1;
                }
            }
            builder.Entity<RoleClaim>().HasData(roleClaimList);
        }
    }
}