using System;
using Capsell.Models.Products;
using Capsell.Models.Shops;

namespace Capsell.Services.Shops
{
    public interface IShopService
    {
        public Task<List<ShopDto>?> GetAllShops();
        public Task<List<ShopDto>?> GetTwoShops();

    }
}

