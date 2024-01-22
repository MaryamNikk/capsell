using System;
using Capsell.Models.Orders;

namespace Capsell.Services.Orders
{
	public interface IOrderService
	{
        public Task<bool> AddProductToCartService(AddToCartDto dto);
        public Task<bool> SendOrderService(string user);
        public Task<OrderDto?> GetUsersCartItemsService(string user);
        public Task<InvoiceDto?> GetInvoiceByIdService(int id, string user);
        public Task<List<InvoiceDto>?> GetInvoiceList(string user);
    }
}

