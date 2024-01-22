using System;
using Capsell.Models.Products;
using Capsell.Services.Products;
using Capsell.Services.Shops;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Capsell.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;
        private readonly ILogger<ShopController> _logger;

        public ShopController(IShopService shopService, ILogger<ShopController> logger)
        {
            _shopService = shopService;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllShops")]
        public async Task<IActionResult> GetAllShops()
        {
            try
            {
                var res = await _shopService.GetAllShops();

                if (res == null || res.Count == 0)
                    return NotFound();

                _logger.LogInformation($"user get product list");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"exception occured in GetAllProducts: {ex.Message}");

                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetTwoProducts")]
        public async Task<IActionResult> GetTwoProducts()
        {
            try
            {
                var res = await _shopService.GetTwoShops();

                if (res == null || res.Count == 0)
                    return NotFound();

                _logger.LogInformation($"user get product list");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"exception occured in GetAllProducts: {ex.Message}");

                return Problem(ex.Message);
            }
        }

    }
}

