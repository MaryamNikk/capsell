using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Capsell.Models.Orders;
using Capsell.Repositories.Orders;
using Capsell.Services.Cache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Capsell.Services.Orders.Tests
{
    public class OrderServiceTests
    {
        [Fact]
        public async Task SendOrderService_WithValidData_ShouldReturnTrue()
        {
            // Arrange
            var orderRepoMock = new Mock<IOrderRepo>();
            var loggerMock = new Mock<ILogger<OrderService>>();
            var configurationMock = new Mock<IConfiguration>();
            var cacheServiceMock = new Mock<ICacheService>();
            var orderService = new OrderService(orderRepoMock.Object, loggerMock.Object, configurationMock.Object, cacheServiceMock.Object);
            var user = "58e97322-46f5-4392-a944-3ecf7323c832";

            var mockCartItem = new ProductItemModel
            {
                ProductId = 1,
                ShopId = 1,
                BaseFee = 15000,
                Count = 2,
                Price = "30000",
                ProductName = "chips"

            };

            orderRepoMock.Setup(repo => repo.GetUsersCartItemsFromDb(It.IsAny<string>()))
                         .ReturnsAsync(new List<ProductItemModel> { mockCartItem });
            orderRepoMock.Setup(repo => repo.AdditemToOrder(It.IsAny<OrderDto>()))
                         .ReturnsAsync(true);

            // Act
            var result = await orderService.SendOrderService(user);

            // Assert
            Assert.True(result);
        }


        [Fact]
        public async Task SendOrderService_WithEmptyCart_ShouldReturnFalse()
        {
            // Arrange
            var orderRepoMock = new Mock<IOrderRepo>();
            var loggerMock = new Mock<ILogger<OrderService>>();
            var configurationMock = new Mock<IConfiguration>();
            var cacheServiceMock = new Mock<ICacheService>();
            var orderService = new OrderService(orderRepoMock.Object, loggerMock.Object, configurationMock.Object, cacheServiceMock.Object);
            var user = "58e97322-46f5-4392-a944-3ecf7323c832";

            orderRepoMock.Setup(repo => repo.GetUsersCartItemsFromDb(It.IsAny<string>()))
                         .ReturnsAsync(new List<ProductItemModel>()); // Mock empty cart for simplicity

            // Act
            var result = await orderService.SendOrderService(user);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task SendOrderService_WithException_ShouldLogErrorAndReturnFalse()
        {
            // Arrange
            var orderRepoMock = new Mock<IOrderRepo>();
            var loggerMock = new Mock<ILogger<OrderService>>();
            var configurationMock = new Mock<IConfiguration>();
            var cacheServiceMock = new Mock<ICacheService>();
            var orderService = new OrderService(orderRepoMock.Object, loggerMock.Object, configurationMock.Object, cacheServiceMock.Object);
            var user = "58e97322-46f5-4392-a944-3ecf7323c832";

            orderRepoMock.Setup(repo => repo.GetUsersCartItemsFromDb(It.IsAny<string>()))
                         .ThrowsAsync(new Exception("Simulated exception"));

            // Act
            var result = await orderService.SendOrderService(user);

            // Assert
            loggerMock.Verify(logger => logger.LogError(It.IsAny<string>()), Times.Once);
            Assert.False(result);
        }

        // Add more tests for other scenarios (e.g., testing caching, different order repo behavior, etc.)...
    }
}
