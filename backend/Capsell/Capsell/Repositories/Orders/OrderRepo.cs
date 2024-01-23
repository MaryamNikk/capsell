using System;
using Capsell.DataProvide;
using Capsell.Models.Orders;
using Microsoft.EntityFrameworkCore;

namespace Capsell.Repositories.Orders
{
    public class OrderRepo : IOrderRepo
    {
        private readonly CapsellDbContext _context;
        private readonly ILogger<OrderRepo> _logger;

        public OrderRepo(CapsellDbContext context,
                         ILogger<OrderRepo> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> AddProductToCartDb(AddToCartDto dto)
        {
            try
            {
                var cartItem = new Cart
                {
                    ProductId = dto.ProductId,
                    Count = dto.Count,
                    UserId = dto.UserId,
                    ShopId = dto.ShopId
                };

                await _context.Carts.AddAsync(cartItem);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"exception occured in AddProductToCartDb: {ex.Message}");

                return false;
            }
        }


        public async Task<List<ProductItemModel>?> GetUsersCartItemsFromDb(string user)
        {
            try
            {
                var items = await (from cartItem in _context.Carts
                                   join product in _context.Products
                                   on cartItem.ProductId equals product.Id
                                   where cartItem.UserId == user
                                   select new ProductItemModel
                                   {
                                       ProductId = product.Id,
                                       ProductName = product.Name,
                                       Count = cartItem.Count,
                                       BaseFee = product.Price,
                                       Price = (cartItem.Count * product.Price).ToString(),
                                       ShopId = product.ShopId
                                   }).ToListAsync();
                if (items != null)
                    return items;
                return new List<ProductItemModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError($"exception occured in GetUsersCartItemsFromDb: {ex.Message}");

                return new List<ProductItemModel>();
            }
        }


        public async Task<bool> AdditemToOrder(OrderDto dto)
        {
            try
            {
                var order = new Order
                {
                    UserId = dto.UserId,
                    Products = dto.ProductsItems,
                    TotalPrice = dto.TotalPrice,
                    ShopId = dto.ShopId
                };

                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"exception occured in AdditemToOrder: {ex.Message}");

                return false;
            }
        }


        public void RemoveItemsFromCart(string user)
        {
            try
            {
                var items = _context.Carts.Where(a => a.UserId == user);
                _context.Carts.RemoveRange(items);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"exception occured in RemoveItemsFromCart: {ex.Message}");
            }
        }


        public async Task<OrderOfUserDto?> GetInvoiceByIdFromDb(int id, string userId)
        {
            try
            {
                var user = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
                var order = await (from o in _context.Orders
                                   where o.Id == id
                                   select new OrderOfUserDto
                                   {
                                       Name = user.Name,
                                       ProductsItems = o.Products,
                                       TotalPrice = o.TotalPrice,
                                   }).FirstOrDefaultAsync();
                if (order != null)
                    return order;
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"exception occured in GetInvoiceByIdFromDb: {ex.Message}");

                return null;
            }
        }


        public async Task<List<OrderOfUserDto>?> GetListOfOrders(string userId)
        {
            try
            {
                var user = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
                var order = await (from o in _context.Orders
                                   select new OrderOfUserDto
                                   {
                                       Name = user.Name,
                                       ProductsItems = o.Products,
                                       TotalPrice = o.TotalPrice,
                                   }).ToListAsync();
                if (order != null)
                    return order;
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"exception occured in GetListOfOrders: {ex.Message}");

                return null;
            }
        }
    }
}

