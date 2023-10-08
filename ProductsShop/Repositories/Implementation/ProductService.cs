using System;
using Microsoft.AspNetCore.Authentication;
using ProductsShop.Data;
using ProductsShop.Models.DTO;
using ProductsShop.Repositories.Abstract;

namespace ProductsShop.Repositories.Implementation
{
    public class ProductService : IProductService
    {
        private readonly DatabaseContext context;

        public ProductService(DatabaseContext context)
        {
            this.context = context;
        }

        readonly Status status = new();
        public async Task<Status> AddAsync(Product model)
        {
            try
            {
                var product = await context.AddAsync(model);
                if (context.Products.Any(x => x.ProductName == model.ProductName))
                {
                    status.StatusCode = 0;
                    status.Message = "Product already exists.";
                    return status;
                }
                await context.SaveChangesAsync();
                status.StatusCode = 1;
                status.Message = "Product added successfully.";
                return status;
            }
            catch (Exception)
            {
                status.StatusCode = 0;
                status.Message = "Error occured! Couldn't add product.";
                return status;
                //throw;
            }
        }

        public async Task<Status> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Status> DetailsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Product? FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetBySearch()
        {
            throw new NotImplementedException();
        }

        public async Task<Status> UpdateAsync(Product model)
        {
            throw new NotImplementedException();
        }
    }
}

