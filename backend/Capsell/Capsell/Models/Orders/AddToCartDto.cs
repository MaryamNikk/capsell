using System;
namespace Capsell.Models.Orders
{
    public class AddToCartDto
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public int ShopId { get; set; }
    }
}

