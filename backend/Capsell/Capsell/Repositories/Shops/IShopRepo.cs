using System;
using Capsell.Models.Products;
using Capsell.Models.Shops;

namespace Capsell.Repositories.Shops
{
	public interface IShopRepo
	{
        public Task<List<ShopDto>?> GetAllShopsFromDb();
    }
}

