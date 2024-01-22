using System;
namespace Capsell.DataProvide
{
	public class ShopCart
	{
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public string? UserId { get; set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual CompaniesProduct Product { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}

