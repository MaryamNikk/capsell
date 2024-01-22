using System;
using Capsell.Models.Products;

namespace Capsell.Services.Products
{
	public interface IProductService
	{
        public Task<bool> AddProductService(AddProductDto dto);
        public Task<List<ProductDto>?> GetAllProducts();
        public Task<List<ProductDto>?> GetAllProductsById(int id);
        public Task<List<ProductDto>?> GetAllProductsByCategory(string category);


    }
}

