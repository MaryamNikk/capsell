using System;
namespace Capsell.Models.Orders
{
    public class OrderDto
	{
		public string ProductsItems { get; set; }
		public string? TotalPrice { get; set; }
		public string? UserId { get; set; }
		public int ShopId { get; set; }
	}
}

