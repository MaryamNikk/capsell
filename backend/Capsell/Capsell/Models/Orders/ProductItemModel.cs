using System;
namespace Capsell.Models.Orders
{
	public class ProductItemModel
	{
		public int ProductId { get; set; }
		public string? ProductName { get; set; }
		public int Count { get; set; }
		public long? BaseFee { get; set; }
        public string? Price { get; set; }
    }
}

