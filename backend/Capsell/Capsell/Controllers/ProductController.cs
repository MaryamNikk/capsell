using System;
using Capsell.Models.Products;
using Capsell.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Capsell.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProduct(AddProductDto dto)
        {
            try
            {
                var res = await _productService.AddProductService(dto);

                _logger.LogInformation($"admin add product");

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"exception occured in AddProduct: {ex.Message}");
                return Problem(ex.Message);
            }
        }


        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var res = await _productService.GetAllProducts();

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
        [Route("GetAllProductsByShop/{id}")]
        public async Task<IActionResult> GetAllProductsByShopId(int id)
        {
            try
            {
                var res = await _productService.GetAllProductsById(id);

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
        [Route("GetAllProductsByCategory/{category}")]
        public async Task<IActionResult> GetTwoProducts(string category)
        {
            try
            {
                var res = await _productService.GetAllProductsByCategory(category);

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

