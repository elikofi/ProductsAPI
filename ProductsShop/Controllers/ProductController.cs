using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsShop.Models.DTO;
using ProductsShop.Repositories.Abstract;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductsShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService service;

        public ProductController(IProductService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route("Add")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Add(Product model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {

                var result = await service.AddAsync(model);
                if (result.StatusCode == 1)
                {
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("id:guid")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update([FromRoute] Guid id, Product model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var product = await service.UpdateAsync(model);
            if (product.StatusCode == 1)
            {
                return Ok(product);
            }
            return BadRequest();
        }


        [HttpDelete]
        [Route("Delete")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await service.DeleteAsync(id);
            if (product.StatusCode == 1)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var data = service.GetAll().OrderBy(data => data.ProductName).ToList();
            return Ok(data);
        }
        //[HttpGet]
        //[Route("FindById")]
        //public IActionResult FindById(Guid id)
        //{
        //    var result = service.FindById(id);
        //    return Ok(result);
        //}
    }
}


