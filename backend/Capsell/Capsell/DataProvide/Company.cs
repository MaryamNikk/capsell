using System;
namespace Capsell.DataProvide
{
	public class Company
	{
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? PhotoUrl { get; set; }
        public long LicenseNumber { get; set; }
        public string? UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<CompaniesProduct> CompaniesProducts { get; set; }
        public virtual ICollection<ShopOrder> Orders { get; set; }
        public virtual ICollection<ShopCart> CartItems { get; set; }
    }
}

