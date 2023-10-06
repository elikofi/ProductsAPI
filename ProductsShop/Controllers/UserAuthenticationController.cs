using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsShop.Models.DTO;
using ProductsShop.Repositories.Abstract;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductsShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAuthenticationController : ControllerBase
    {
        private readonly IUserAuthenticationService service;

        public UserAuthenticationController(IUserAuthenticationService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(Login model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await service.LoginAsync(model);
            if (result.StatusCode == 1)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest();
            }
        }


        [Authorize]
        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await service.LogoutAsync();
            return Ok();
        }

        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Registration(Registration model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            model.Role = "user";
            //_ = new Registration
            //{
            //    Username = model.Username,
            //    Name = model.Name,
            //    Email = model.Email,
            //    Password = model.Password,
            //    Role = model.Role
            //};
            var result = await service.RegistrationAsync(model);

            return Ok(result);
        }

        //Admin
        [HttpGet(Name = "Admin register")]
        public async Task<IActionResult> Register()
        {
            var model = new Registration
            {
                Username = "admin",
                Name = "Elijah Amoako",
                Email = "elijahamoako72@gmail.com",
                Password = "Elijah@12345#",
                Role = "admin"
            };
            var result = await service.RegistrationAsync(model);
            return Ok(result);
        }
    }
}

