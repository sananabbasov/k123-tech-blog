using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.ViewComponents
{
	public class DashboardAuthViewComponent : ViewComponent
	{
		private readonly IHttpContextAccessor _httpContext;
		private readonly UserManager<User> _userManager;


        public DashboardAuthViewComponent(IHttpContextAccessor httpContext, UserManager<User> userManager)
        {
            _httpContext = httpContext;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
		{
			var userId = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			var user = await _userManager.FindByIdAsync(userId);
			return View("DashboardAuth", user);
		}
	}
}

