using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using ProductsShop.Models.Domain;
using ProductsShop.Models.DTO;
using ProductsShop.Repositories.Abstract;

namespace ProductsShop.Repositories.Implementation
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserAuthenticationService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        readonly Status status = new();
        public async Task<Status> LoginAsync(Login model)
        {
            //for incorrect username
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                status.StatusCode = 0;
                status.Message = "Invalid username.";
                return status;
            }

            //for incorrect password

            if (!await userManager.CheckPasswordAsync(user, model.Password))
            {
                status.StatusCode = 0;
                status.Message = "Incorrect password.";
                return status;
            }

            //if both are correct

            var signIn = await signInManager.PasswordSignInAsync(user, model.Password, false, true);
            if (signIn.Succeeded)
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new(ClaimTypes.Name, user.UserName)
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                status.StatusCode = 1;
                status.Message = "Login successful.";
                return status;
            }
            else if (signIn.IsLockedOut)
            {
                status.StatusCode = 0;
                status.Message = "User logged out.";
                return status;
            }
            else
            {
                status.StatusCode = 0;
                status.Message = "Login not successful.";
                return status;
            }
        }

        public async Task<Status> LogoutAsync()
        {

            await signInManager.SignOutAsync();
            status.StatusCode = 1;
            status.Message = "Signed out.";
            return status;
        }

        public async Task<Status> RegistrationAsync(Registration model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "User already exists.";
                return status;
            }

            ApplicationUser user = new()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                Name = model.Name,
                Email = model.Email,
                UserName = model.Username,
                EmailConfirmed = true
                
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "Unable to create user.";
                return status;
            }

            if (!await roleManager.RoleExistsAsync(model.Role))
            {
                await roleManager.CreateAsync(new IdentityRole(model.Role));
            }

            if (await roleManager.RoleExistsAsync(model.Role))
            {
                await userManager.AddToRoleAsync(user, model.Role);
            }

            status.StatusCode = 1;
            status.Message = "User Registered!";
            return status;
        }
    }
}

