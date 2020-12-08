using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StartupProject_Asp.NetCore_PostGRE.AuthorizationRequirement;
using StartupProject_Asp.NetCore_PostGRE.Data.Enums;
using StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity;
using StartupProject_Asp.NetCore_PostGRE.Data.Seeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartupProject_Asp.NetCore_PostGRE.Controllers.SuperAdmin
{
    [AuthorizePolicy(EClaim.SuperAdmin_All)]
    public class RoleManagerController : Controller
    {
        private readonly RoleManager<Role> _roleManager;
        public RoleManagerController(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            List<Role> roles = await _roleManager.Roles
                                //.Select(r => new { r.Id, r.Name, r.Description })
                                //.Distinct()
                                .OrderBy(r => r.Name)//Ascending
                                .ThenByDescending(r => r.Description)//Descending
                                .ToListAsync();
            #region SuperAdmin role hide from list so that it can't be modified
            Role superAdminRole = roles.Where(r => r.Name == RoleSeeder.SuperAdminRoleName).FirstOrDefault();
            if(superAdminRole!=null)
            {
                roles.Remove(superAdminRole);
            }
            #endregion
            return View(roles);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName, string roleDescription)
        {
            if (roleName != null)
            {
                //await _roleManager.CreateAsync(new Role(roleName.Trim()));
                await _roleManager.CreateAsync(new Role {
                    Name = roleName.Trim(),
                    Description = roleDescription.Trim()
                });
                //var accountRole = await _roleManager.FindByNameAsync(roleName);
                //foreach (object name in Enum.GetValues(typeof(EClaim)))
                //{
                //    string description = ((EClaim)name).Description();
                //    await _roleManager.AddClaimAsync(accountRole, new Claim(name.ToString(), description));
                //}
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteRole(Guid id)
        {
            Role role = _roleManager.Roles.Where(r => r.Id == id).FirstOrDefault();
            if(role!=null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index", "RoleManager", new { @id = id });

        }
    }
}
