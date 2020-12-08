using Microsoft.EntityFrameworkCore;
using StartupProject_Asp.NetCore_PostGRE.Data.Enums;
using StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity;
using System;
using System.Collections.Generic;

namespace StartupProject_Asp.NetCore_PostGRE.Data.Seeds
{
    public class RoleSeeder
    {
        internal static void Execute(ModelBuilder builder, ICollection<Guid> superAdminUserIdList)
        {
            IList<Role> roleList = new List<Role>();
            IList<UserRole> userRoleList = new List<UserRole>();
            foreach (Guid superAdminId in superAdminUserIdList)
            {
                Guid roleId = Guid.NewGuid();
                string roleName = "Super-Admin";
                roleList.Add(new Role
                {
                    Id = roleId,
                    Name = roleName,
                    NormalizedName = roleName.ToString().Normalize().ToUpper(),
                    Description = DateTime.UtcNow.ToString()
                });
                userRoleList.Add(new UserRole
                {
                    RoleId = roleId,
                    UserId = superAdminId,
                    ReasonForAdding = "Created During Migration"
                });
            }
            
            builder.Entity<Role>().HasData(roleList);
            builder.Entity<UserRole>().HasData(userRoleList);
        }
    }
}
