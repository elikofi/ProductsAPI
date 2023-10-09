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

        public async Task<Status> UpdateAsync(Product model)
        {
            try
            {
                context.Products.Update(model);
                await context.SaveChangesAsync();
                status.StatusCode = 1;
                status.Message = "Product updated successfully.";
                return status;
            }
            catch (Exception)
            {
                status.StatusCode = 0;
                status.Message = "Error occured! Couldn't update product.";
                return status;
            }
        }
        public async Task<Status> DeleteAsync(Guid id)
        {
            try
            {
                var product = await context.Products.FindAsync(id);
                if (product == null)
                {
                    status.StatusCode = 0;
                    status.Message = "Product not found.";
                    return status;
                }
                context.Products.Remove(product);
                await context.SaveChangesAsync();
                status.StatusCode = 1;
                status.Message = "Product deleted successfully.";
                return status;
            }
            catch (Exception)
            {
                status.StatusCode = 0;
                status.Message = "Error occured! Couldn't delete product.";
                return status;
            }
        }


        public Product? FindById(Guid id)
        {
            return context.Products.Find(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return context.Products.ToList();
        }

        //public IEnumerable<Product> GetBySearch()
        //{
        //    throw new NotImplementedException();
        //}

        
    }
}

