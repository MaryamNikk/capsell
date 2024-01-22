using System;
namespace Capsell.DataProvide
{
	public class Order
	{
        public int Id { get; set; }
        public string? Products { get; set; }
        public string? TotalPrice { get; set; }
        public string? UserId { get; set; }
        public int ShopId { get; set; }

        public virtual Shop Shop { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}

