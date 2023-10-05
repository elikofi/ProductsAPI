using System;
using Microsoft.AspNetCore.Identity;

namespace ProductsShop.Models.Domain
{
	public class ApplicationUser : IdentityUser
	{
		public string Name { get; set; }

		public string? ProfilePic { get; set; }
	}
}
	
