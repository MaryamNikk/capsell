using System;
using Capsell.DataProvide;
using Capsell.Models.Products;
using Capsell.Models.Shops;
using Microsoft.EntityFrameworkCore;

namespace Capsell.Repositories.Products
{
	public class ProductRepo : IProductRepo
	{
        private readonly CapsellDbContext _Context;
        private readonly ILogger<ProductRepo> _logger;

        public ProductRepo(CapsellDbContext context, ILogger<ProductRepo> logger)
        {
            _Context = context;
            _logger = logger;
        }

        public async Task<bool> AddProductToDb(AddProductDto dto)
        {
            try
            {
                var product = new Product
                {
                    Name = dto.Name,
                    Price = dto.Price,
                    PhotoUrl = dto.ImageUrl,
                    Count = dto.Count,
                    Category = dto.Category,
                    ShopId = dto.ShopId
                };

                await _Context.Products.AddAsync(product);
                await _Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"exception occured in AddProductToDb: {ex.Message}");

                return false;
            }
        }

        public async Task<List<ProductDto>> GetAllProductsFromDb()
        {
            try
            {
                var products = await (from product in _Context.Products
                                      select new ProductDto
                                      {
                                          Id = product.Id,
                                          Name = product.Name,
                                          Price = product.Price,
                                          ImageUrl = product.PhotoUrl,
                                          Count = product.Count,
                                          ShopsDto = (from shop in _Context.Shops
                                                      where shop.Id == product.ShopId
                                                      select new ShopDto
                                                      {
                                                          Id = shop.Id,
                                                          Name = shop.Name,
                                                          Description = shop.Description,
                                                          ImageUrl = shop.PhotoUrl
                                                      }).FirstOrDefault(),
                                          Category = product.Category,
                                      }).ToListAsync();


                if (products != null)
                {
                    return products;
                }
                return new List<ProductDto>();

            }
            catch (Exception ex)
            {
                _logger.LogError($"exception occured in GetAllProductsFromDb: {ex.Message}");

                return new List<ProductDto>();
            }


        }
    }
}

