using Microsoft.EntityFrameworkCore;
using StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity;
using System;
using System.Collections.Generic;

namespace StartupProject_Asp.NetCore_PostGRE.Data.Seeds
{
    public class RoleSeeder
    {
        internal static string SuperAdminRoleName = "Super-Admin";
        internal static IList<Guid> Execute(ModelBuilder builder, ICollection<Guid> superAdminUserIdList)
        {
            IList<Role> roleList = new List<Role>();
            IList<UserRole> userRoleList = new List<UserRole>();
            IList<Guid> roleIdList = new List<Guid>();
            foreach (Guid superAdminId in superAdminUserIdList)
            {
                Guid roleId = Guid.NewGuid();
                roleList.Add(new Role
                {
                    Id = roleId,
                    Name = SuperAdminRoleName,
                    NormalizedName = SuperAdminRoleName.ToString().Normalize().ToUpper(),
                    Description = DateTime.UtcNow.ToString()
                });
                userRoleList.Add(new UserRole
                {
                    RoleId = roleId,
                    UserId = superAdminId,
                    ReasonForAdding = "Created During Migration"
                });
                roleIdList.Add(roleId);
            }
            
            builder.Entity<Role>().HasData(roleList);
            builder.Entity<UserRole>().HasData(userRoleList);
            return roleIdList;
        }
    }
}
