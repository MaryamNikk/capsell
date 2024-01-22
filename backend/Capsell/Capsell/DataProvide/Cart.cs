using System;
namespace Capsell.DataProvide
{
	public class Cart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public string? UserId { get; set; }
        public int ShopId { get; set; }

        public virtual Shop Shop { get; set; }
        public virtual Product Product { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}

