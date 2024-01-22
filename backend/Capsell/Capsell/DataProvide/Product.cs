using System;
namespace Capsell.DataProvide
{
	public class Product
	{

        public Product()
        {
            CartItems = new HashSet<Cart>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public long Price { get; set; }
        public int Count { get; set; }
        public string? PhotoUrl { get; set; }
        public int ShopId { get; set; }
        public string? Category { get; set; }

        public virtual Shop Shop { get; set; }
        public virtual ICollection<Cart> CartItems { get; set; }

    }
}

