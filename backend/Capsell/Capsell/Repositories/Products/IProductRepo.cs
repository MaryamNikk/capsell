using System;
using Capsell.Models.Products;

namespace Capsell.Repositories.Products
{
	public interface IProductRepo
	{
        public Task<bool> AddProductToDb(AddProductDto dto);
        public Task<List<ProductDto>> GetAllProductsFromDb();
    }
}

