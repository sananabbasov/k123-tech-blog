using System;
using Microsoft.AspNetCore.Identity;
using WebUI.Models;

namespace WebUI.ViewModels
{
	public class UserRoleVM
	{
		public User User { get; set; }
		public List<IdentityRole> Roles { get; set; }
	}
}

