﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StartupProject_Asp.NetCore_PostGRE.Attributes;
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
    [AuthorizeRoles(ERole.SuperAdmin)]
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
        public async Task<IActionResult> Index()
        {
            List<Role> roles = await _roleManager.Roles.ToListAsync();

            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> Manage(Guid roleId)
        {
            ViewBag.roleId = roleId;
            Role role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }
            //List<RoleClaim> roleClaims = role.Claims.ToList();

            //ViewBag.claims = claims;
            List<ManageRoleClaimsViewModel> model = new List<ManageRoleClaimsViewModel>();

            foreach (object name in Enum.GetValues(typeof(EClaim)))
            {
                string description = ((EClaim)name).Description();
                RoleClaim roleClaim = await _context.RoleClaims
                                        .Where(rc => rc.ClaimType == name.ToString())
                                        .Where(rc => rc.ClaimValue == description)
                                        .Where(rc => rc.RoleId == role.Id)
                                        .FirstOrDefaultAsync();
                ManageRoleClaimsViewModel roleClaimsViewModel = new ManageRoleClaimsViewModel {
                    RoleId = role.Id,
                    ClaimType = name.ToString(),
                    ClaimValue = description
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
                model.Add(roleClaimsViewModel);
            }
            return View(model);
        }

        /*[HttpPost]
        public async Task<IActionResult> Manage(List<ManageUserRolesViewModel> model, string roleId)
        {
            Role role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
            {
                return View();
            }

            var roleClaim = await _context.RoleClaims
                                        .Where(rc => rc.RoleId == role.Id)
                                        .FirstOrDefaultAsync();
            _context.RoleClaims.RemoveRange(roleClaim);
            //var result = await _context.SaveChangesAsync();
            foreach(var a in model)
            {
            }
            var result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));
            IdentityResult result2 = await _userManager.UpdateSecurityStampAsync(user);    //Forcely Logout User

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }
            if (!result2.Succeeded)
            {
                ModelState.AddModelError("", "Cannot log out the user");
                return View(model);
            }
            return RedirectToAction("Index");
        }*/
    }
}