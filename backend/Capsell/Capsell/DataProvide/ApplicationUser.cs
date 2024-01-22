using System;
using Microsoft.AspNetCore.Identity;

namespace Capsell.DataProvide
{
	public class ApplicationUser : IdentityUser
	{
        public ApplicationUser()
        {
            CartItems = new HashSet<Cart>();
            Orders = new HashSet<Order>();
            ShopCartItems = new HashSet<ShopCart>();
            ShopOrders = new HashSet<ShopOrder>();
        }

        public string? Role { get; set; }

        public string? Name { get; set; }

        public string? Address { get; set; }

        public virtual ICollection<Cart> CartItems { get; set; }
        public virtual ICollection<ShopCart> ShopCartItems { get; set; }
        public virtual Shop? Shop { get; set; }
        public virtual Company? Company { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ShopOrder> ShopOrders { get; set; }

    }
}

