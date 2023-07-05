using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebUI.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();   
        }

        [HttpPost]
        public async Task<IActionResult> Create(string role)
        {
            var findRole = await _roleManager.FindByNameAsync(role);

            if (findRole != null)
            {
                return View();
            }
            IdentityRole identityRole = new()
            {
                Name = role
            };
            await _roleManager.CreateAsync(identityRole);
            return RedirectToAction("Index");
        }
    }
}

