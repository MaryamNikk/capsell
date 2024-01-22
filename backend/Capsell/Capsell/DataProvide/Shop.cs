using System;
using System.ComponentModel;

namespace Capsell.DataProvide
{
	public class Shop
	{
        public Shop()
        {
            CartItems = new HashSet<Cart>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? PhotoUrl { get; set; }
        public long RegistrationLicenseNumber { get; set; }
        public string? UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Cart> CartItems { get; set; }
    }
}

