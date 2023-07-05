using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;
using WebUI.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebUI.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [Authorize(Roles="Admin, Moderator")]
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRole(string id)
        {
            var findUser = await _userManager.FindByIdAsync(id);
            var roles = _roleManager.Roles.ToList();
            UserRoleVM vm = new()
            {
                User = findUser,
                Roles = roles
            };
            return View(vm);
        }


        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddRole(string roleName, string userId)
        {
            var findUser = await _userManager.FindByIdAsync(userId);
            var userRole = await _userManager.GetRolesAsync(findUser);
            await _userManager.RemoveFromRolesAsync(findUser,userRole);

            var addRole = await _userManager.AddToRoleAsync(findUser,roleName);

            if (addRole.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}

