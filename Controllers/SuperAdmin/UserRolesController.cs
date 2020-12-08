using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StartupProject_Asp.NetCore_PostGRE.AuthorizationRequirement;
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
    public class UserRolesController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserRolesController(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            List<User> users = await _userManager.Users.OrderBy(u => u.FirstName).ThenBy(u=> u.LastName).ToListAsync();
            List<UserRolesViewModel> userRolesViewModel = new List<UserRolesViewModel>();
            foreach (User user in users)
            {
                UserRolesViewModel thisViewModel = new UserRolesViewModel();
                thisViewModel.UserId = user.Id;
                thisViewModel.Email = user.Email;
                thisViewModel.FirstName = user.FirstName;
                thisViewModel.LastName = user.LastName;
                thisViewModel.Roles = await GetUserRoles(user);
                userRolesViewModel.Add(thisViewModel);
            }
            return View(userRolesViewModel);
        }
        private async Task<List<string>> GetUserRoles(User user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }
        [HttpGet]
        public async Task<IActionResult> Manage(Guid userId)
        {
            ViewBag.userId = userId;
            User user = await _userManager.FindByIdAsync(userId.ToString());
            IList<string> userRoleList = await _userManager.GetRolesAsync(user);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }
            ViewBag.UserName = user.UserName;
            List<ManageUserRolesViewModel> model = new List<ManageUserRolesViewModel>();

            foreach (Role role in _roleManager.Roles.OrderBy(r=>r.Name))
            {
                ManageUserRolesViewModel userRolesViewModel = new ManageUserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                //if (await _userManager.IsInRoleAsync(user, role.Name))
                if(userRoleList.Contains(role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                model.Add(userRolesViewModel);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Manage(List<ManageUserRolesViewModel> model, string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View();
            }
            IList<string> roles = await _userManager.GetRolesAsync(user);
            IdentityResult result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }
            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));
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
        }
    }
}
