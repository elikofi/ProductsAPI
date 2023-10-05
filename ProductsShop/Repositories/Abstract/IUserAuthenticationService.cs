using System;
using ProductsShop.Models.DTO;

namespace ProductsShop.Repositories.Abstract
{
	public interface IUserAuthenticationService
	{
		Task<Status> RegistrationAsync(Registration model);

		Task<Status> LoginAsync(Login model);

		Task<Status> LogoutAsync();
	}
}

