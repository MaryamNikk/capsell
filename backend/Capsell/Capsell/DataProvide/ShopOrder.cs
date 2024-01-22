using System;
namespace Capsell.DataProvide
{
	public class ShopOrder
	{
        public int Id { get; set; }
        public string? Products { get; set; }
        public string? TotalPrice { get; set; }
        public string? UserId { get; set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}

