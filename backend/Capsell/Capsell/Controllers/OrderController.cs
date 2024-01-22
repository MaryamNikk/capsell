using System;
using Capsell.Models.Orders;
using Capsell.Services.Orders;
using Microsoft.AspNetCore.Mvc;

namespace Capsell.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpPost]
        [Route("AddToCart")]
        public async Task<IActionResult> AddProductToCart(AddToCartDto dto)
        {
            try
            {

                var res = await _orderService.AddProductToCartService(dto);

                _logger.LogInformation($"user: {dto.UserId} add product to cart");


                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"exception occured in AddToCart: {ex.Message}");
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [Route("SendOrder/{id}")]
        public async Task<IActionResult> SendOrder(string id)
        {
            try
            {
                var res = await _orderService.SendOrderService(id);

                _logger.LogInformation($"user: {id} send an order");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"exception occured in SendOrder: {ex.Message}");
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetInvoiceById/{id}/{userId}")]
        public async Task<IActionResult> GetInvoiceById(string id, string userId)
        {
            try
            {

                var res = await _orderService.GetInvoiceByIdService(int.Parse(id), userId);

                _logger.LogInformation($"user: {userId} get Invoice with id: {id}");


                if (res == null)
                    return NotFound();

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"exception occured in GetInvoiceById: {ex.Message}");
                return Problem(ex.Message);
            }
        }


        [HttpGet]
        [Route("GetInvoiceList/{id}")]
        public async Task<IActionResult> GetInvoiceList(string id)
        {
            try
            {
                var res = await _orderService.GetInvoiceList(id);

                _logger.LogInformation($"user: {id} get Invoice list");

                if (res == null)
                    return NotFound();

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"exception occured in GetInvoiceList: {ex.Message}");
                return Problem(ex.Message);
            }
        }
    }

}
