using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StartupProject_Asp.NetCore_PostGRE.AuthorizationRequirement;
using StartupProject_Asp.NetCore_PostGRE.Data;
using StartupProject_Asp.NetCore_PostGRE.Data.Enums;
using StartupProject_Asp.NetCore_PostGRE.Data.Models.Identity;
using StartupProject_Asp.NetCore_PostGRE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartupProject_Asp.NetCore_PostGRE.Controllers.SuperAdmin
{
    [AuthorizePolicy(EClaim.SuperAdmin_All)]
    public class RoleClaimController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<Role> _roleManager;

        public RoleClaimController(RoleManager<Role> roleManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Manage(Guid id)
        {
            Role role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                return View("Error");
            }
            ViewBag.role = role;
            Dictionary<string, List<ManageRoleClaimsViewModel>> model = new Dictionary<string, List<ManageRoleClaimsViewModel>>();

            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();


            foreach (EClaim claim in Enum.GetValues(typeof(EClaim)))
            {
                #region Skip Superadmin permission giving for any role
                bool condition = claim.ToString().Length < 11 ? false : claim.ToString().Substring(0, 11) == "SuperAdmin_";
                if (condition)
                {
                    continue;
                }
                #endregion
                RoleClaim roleClaim = await _context.RoleClaims
                                        .Where(rc => rc.ClaimType == claim.ToString())
                                        .Where(rc => rc.ClaimValue == claim.Description())
                                        .Where(rc => rc.RoleId == role.Id)
                                        .FirstOrDefaultAsync();
                ManageRoleClaimsViewModel roleClaimsViewModel = new ManageRoleClaimsViewModel {
                    RoleId = role.Id,
                    ClaimType = claim.ToString(),
                    ClaimValue = claim.Description()
                };
                if(roleClaim==null)
                {
                    roleClaimsViewModel.Selected = false;
                }
                else
                {
                    roleClaimsViewModel.Id = roleClaim.Id;
                    roleClaimsViewModel.Selected = true;
                }
                string moduleName = claim.ToString().Split("_").FirstOrDefault();
                if (!model.ContainsKey(moduleName))
                {
                    model.Add(moduleName, new List<ManageRoleClaimsViewModel>());
                }
                model[moduleName].Add(roleClaimsViewModel);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Manage(List<ManageRoleClaimsViewModel> model, Guid roleId)
        {
            Role role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
            {
                return View();
            }

            var roleClaim = await _context.RoleClaims
                                        .Where(rc => rc.RoleId == role.Id)
                                        .ToListAsync();
            _context.RoleClaims.RemoveRange(roleClaim);
            //await _context.SaveChangesAsync();
            foreach (ManageRoleClaimsViewModel entry in model)
            {
                if(entry.Selected == true)
                { 
                    _context.RoleClaims.Add(new RoleClaim {
                         Role = role,
                         RoleId = roleId,
                         ClaimType = entry.ClaimType,
                         ClaimValue= entry.ClaimValue
                    });
                }
            }
            var result = await _context.SaveChangesAsync();
            //return RedirectToAction("Index");
            return RedirectToAction("Index", "RoleManager", new { area = "" });
        }
    }
}
