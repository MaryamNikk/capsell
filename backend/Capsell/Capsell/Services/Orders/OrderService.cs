using System;
using System.Diagnostics;
using Capsell.Models.Orders;
using Capsell.Models.Products;
using Capsell.Repositories.Orders;
using Capsell.Services.Cache;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Capsell.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepo;
        private readonly ILogger<OrderService> _logger;
        private readonly IConfiguration _configuration;
        private readonly ICacheService _cacheService;

        public OrderService(IOrderRepo orderRepo, ILogger<OrderService> logger,
                            IConfiguration configuration, ICacheService cacheService)
        {
            _orderRepo = orderRepo;
            _logger = logger;
            _configuration = configuration;
            _cacheService = cacheService;
        }

        public async Task<bool> AddProductToCartService(AddToCartDto dto)
        {
            var IsItemAddedToCart = await _orderRepo.AddProductToCartDb(dto);
            return IsItemAddedToCart;
        }

        public async Task<bool> SendOrderService(string user)
        {
            try
            {
                var item = await GetUsersCartItemsService(user);
                var isAdded = await _orderRepo.AdditemToOrder(item);
                _orderRepo.RemoveItemsFromCart(user);

                if (!string.IsNullOrEmpty(item.ProductsItems))
                {
                    SetDataToCache(item.ProductsItems);
                }

                _ = Task.Run(RunTruffleCommands);
                //await RunTruffleCommands();




                return isAdded;
            }
            catch (Exception ex)
            {
                _logger.LogError($"exception occured in SendOrderService: {ex.Message}");
                return false;
            }
        }

        private async Task RunTruffleCommands()
        {
            try
            {
                string workingDirectory = "../../../../../../Desktop/deploy-contract/myContractProject";

                // Build and run the commands
                string[] commands = new string[]
                {
                    "truffle migrate --network goerli"
                };

                foreach (string command in commands)
                {
                    await RunCommand(command, workingDirectory);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred while running Truffle commands: {ex.Message}");
            }
        }

        private async Task RunCommand(string command, string workingDirectory)
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = "/bin/bash";
                process.StartInfo.Arguments = $"-c \"{command}\"";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WorkingDirectory = workingDirectory;


                process.Start();

                string output = await process.StandardOutput.ReadToEndAsync();
                string error = await process.StandardError.ReadToEndAsync();

                process.WaitForExit();

                _logger.LogInformation($"Command: {command}");
                _logger.LogInformation($"Output: {output}");
                _logger.LogInformation($"Error: {error}");
            }
        }


        //set data to cache with expiration date
        private void SetDataToCache(string value)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    var expirationTime = GetExpirationTime();
                    _cacheService.SetData("initialInvoice", value, expirationTime);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"exception occured in SetDataToCache: {ex.Message}");
            }
        }


        private DateTimeOffset GetExpirationTime()
        {
            try
            {
                var expire = _configuration.GetValue<double>("KeyExiratonTime");
                var time = DateTimeOffset.Now.AddMinutes(expire);
                return time;
            }
            catch (Exception ex)
            {
                _logger.LogError($"exception occured in getting KeyExiratonTime from appSetting: {ex.Message}");
                return DateTimeOffset.Now.AddMinutes(1.0); ;
            }
        }

        public async Task<OrderDto?> GetUsersCartItemsService(string user)
        {
            try
            {
                var userCartItems = await _orderRepo.GetUsersCartItemsFromDb(user);
                long totalPrice = 0;
                foreach (var i in userCartItems)
                {
                    totalPrice += long.Parse(i.Price);
                }
                var serializedItems = JsonConvert.SerializeObject(userCartItems).ToString();
                var OrderItem = new OrderDto
                {
                    ProductsItems = serializedItems,
                    TotalPrice = totalPrice.ToString(),
                    UserId = user,
                    ShopId = userCartItems[0].ShopId   
                };

                if (OrderItem != null)
                    return OrderItem;

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"exception occured in GetUsersCartItemsService: {ex.Message}");

                return null;
            }
        }

        public async Task<InvoiceDto?> GetInvoiceByIdService(int id, string user)
        {
            try
            {
                var orderDetail = await _orderRepo.GetInvoiceByIdFromDb(id, user);

                if (orderDetail == null)
                    return null;

                var invoice = new InvoiceDto
                {
                    Name = orderDetail.Name,
                    ProductListInInvoice = JsonConvert.DeserializeObject<List<ProductItemModel>>(orderDetail.ProductsItems),
                    TotalPrice = orderDetail.TotalPrice,
                };

                return invoice;
            }
            catch (Exception ex)
            {
                _logger.LogError($"exception occured in GetInvoiceByIdService: {ex.Message}");
                return null;

            }
        }

        public async Task<List<InvoiceDto>?> GetInvoiceList(string user)
        {
            try
            {
                var orderList = await _orderRepo.GetListOfOrders(user);

                if (orderList == null || orderList.Count == 0)
                    return null;

                var invoice = (from order in orderList
                               select new InvoiceDto
                               {
                                   Name = order.Name,
                                   ProductListInInvoice = JsonConvert.DeserializeObject<List<ProductItemModel>>(order.ProductsItems),
                                   TotalPrice = order.TotalPrice,
                               });

                return invoice.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"exception occured in GetInvoiceList: {ex.Message}");
                return null;
            }
        }

    }
}

