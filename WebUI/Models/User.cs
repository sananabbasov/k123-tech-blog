using System;
using Microsoft.AspNetCore.Identity;

namespace WebUI.Models
{
	public class User : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PhotoUrl { get; set; }
		public string About { get; set; }
		public string ConfirmationToken { get; set; }
		public DateTime ConfirmationTokenEndDate { get; set; }
	}
}

