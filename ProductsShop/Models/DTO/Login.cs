using System;
using System.ComponentModel.DataAnnotations;

namespace ProductsShop.Models.DTO
{
	public class Login
	{
		[Required]
		public string UserName { get; set; }
		[Required]
		public string Password { get; set; }
	}
}

