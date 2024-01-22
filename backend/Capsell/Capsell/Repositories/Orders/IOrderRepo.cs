using System;
using Capsell.Models.Orders;

namespace Capsell.Repositories.Orders
{
	public interface IOrderRepo
	{
        public Task<bool> AdditemToOrder(OrderDto item);
        public Task<bool> AddProductToCartDb(AddToCartDto dto);
        public Task<OrderOfUserDto?> GetInvoiceByIdFromDb(int id, string user);
        public Task<List<OrderOfUserDto>?> GetListOfOrders(string user);
        public Task<List<ProductItemModel>?> GetUsersCartItemsFromDb(string user);
        public void RemoveItemsFromCart(string user);
    }
}

