using System;
namespace Capsell.Models.Products
{
	public class AddProductDto
	{
        public string? Name { get; set; }
        public long Price { get; set; }
        public int Count { get; set; }
        public string? ImageUrl { get; set; }
        public string? Category { get; set; }
        public int ShopId { get; set; }
    }
}

