using System;
using ProductsShop.Models.DTO;

namespace ProductsShop.Repositories.Abstract
{
	public interface IProductService
	{
        Task<Status> AddAsync(Product model);

        Task<Status> UpdateAsync(Product model);

        Task<Status> DeleteAsync(Guid id);

        Product? FindById(Guid id);

        public IEnumerable<Product> GetAll();

        //public IEnumerable<Product> GetBySearch();
    }
}

