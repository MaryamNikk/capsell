using System;
using Capsell.Models.Products;
using Capsell.Models.Shops;
using Capsell.Repositories.Products;
using Capsell.Repositories.Shops;
using Capsell.Services.Products;

namespace Capsell.Services.Shops
{
	public class ShopService : IShopService
    {
        private readonly IShopRepo _shopRepo;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ShopService> _logger;

        public ShopService(IShopRepo shopRepo, IConfiguration configuration,
                              ILogger<ShopService> logger)
        {
            _shopRepo = shopRepo;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<List<ShopDto>?> GetAllShops()
        {
            try
            {
                var products = await _shopRepo.GetAllShopsFromDb();
                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError($"exception occured in GetAllShops: {ex.Message}");
                return new List<ShopDto>();
            }
        }

        public async Task<List<ShopDto>?> GetTwoShops()
        {
            try
            {
                var products = await _shopRepo.GetAllShopsFromDb();
                return products?.Take(2).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"exception occured in GetTwoShops: {ex.Message}");
                return new List<ShopDto>(); ;
            }
        }
    }
}


