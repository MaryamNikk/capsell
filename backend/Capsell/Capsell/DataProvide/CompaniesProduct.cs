using System;
namespace Capsell.DataProvide
{
	public class CompaniesProduct
	{
        public CompaniesProduct()
        {
            CartItems = new HashSet<ShopCart>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public long Price { get; set; }
        public int Count { get; set; }
        public string? PhotoUrl { get; set; }
        public int CompanyId { get; set; }
        public string? Category { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<ShopCart> CartItems { get; set; }
    }
}

