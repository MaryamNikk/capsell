using System;
using Capsell.Models.Products;
using Capsell.Repositories.Products;

namespace Capsell.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepo productRepo, IConfiguration configuration,
                              ILogger<ProductService> logger)
        {
            _productRepo = productRepo;
            _configuration = configuration;
            _logger = logger;
        }


        public async Task<bool> AddProductService(AddProductDto dto)
        {
            try
            {
                return await _productRepo.AddProductToDb(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"exception occured in AddProductService: {ex.Message}");
                return false;

            }
        }

        public async Task<List<ProductDto>?> GetAllProducts()
        {
            try
            {
                var products = await _productRepo.GetAllProductsFromDb();
                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError($"exception occured in GetAllProducts: {ex.Message}");
                return null;
            }
        }

        public async Task<List<ProductDto>?> GetAllProductsById(int id)
        {
            try
            {
                var products = await _productRepo.GetAllProductsFromDb();
                return products.Where(p => p.ShopsDto?.Id == id).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"exception occured in GetTwoProducts: {ex.Message}");
                return null;
            }
        }

        public async Task<List<ProductDto>?> GetAllProductsByCategory(string category)
        {
            try
            {
                var products = await _productRepo.GetAllProductsFromDb();
                return products.Where(p => p.Category == category).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"exception occured in GetTwoProducts: {ex.Message}");
                return null;
            }
        }
    }
}

