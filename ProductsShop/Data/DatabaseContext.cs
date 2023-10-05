using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductsShop.Models.Domain;

namespace ProductsShop.Data
{
	public class DatabaseContext : IdentityDbContext<ApplicationUser>
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
		{

		}
	}
}

