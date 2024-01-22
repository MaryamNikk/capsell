using System;
using Capsell.DataProvide;
using Capsell.Models.Products;
using Capsell.Models.Shops;
using Capsell.Repositories.Products;
using Capsell.Services.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Capsell.Repositories.Shops
{
    public class ShopRepo : IShopRepo
    {
        private readonly CapsellDbContext _Context;
        private readonly ILogger<ShopRepo> _logger;

        public ShopRepo(CapsellDbContext context, ILogger<ShopRepo> logger)
        {
            _Context = context;
            _logger = logger;
        }

        public async Task<List<ShopDto>?> GetAllShopsFromDb()
        {
            try
            {
                var shops = await (from shop in _Context.Shops
                                      select new ShopDto
                                      {
                                          Id = shop.Id,
                                          Name = shop.Name,
                                          Description = shop.Description,
                                          ImageUrl = shop.PhotoUrl
                                      }).ToListAsync();


                if (shops != null)
                {
                    return shops;
                }
                return new List<ShopDto>();

            }
            catch (Exception ex)
            {
                _logger.LogError($"exception occured in GetAllProductsFromDb: {ex.Message}");

                return new List<ShopDto>();
            }

        }
    }
}

