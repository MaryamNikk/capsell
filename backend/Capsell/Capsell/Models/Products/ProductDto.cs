using System;
using Capsell.Models.Shops;

namespace Capsell.Models.Products
{
	public class ProductDto
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public int Count { get; set; }
		public long Price { get; set; }
		public string? ImageUrl { get; set; }
		public string? Category { get; set; }

		public ShopDto? ShopsDto { get; set; }
	}
}

