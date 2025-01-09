
using Grpc.Core;

namespace ECommerce.Basket.API

{
    public class CustomBasketService : BasketService.BasketServiceBase
    {
        private readonly ILogger<CustomBasketService> _logger;

        public CustomBasketService(ILogger<CustomBasketService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerBasket> GetBasket(GetBasketRequest request, ServerCallContext context)
        {
            _logger.LogInformation("GetBasket rpc fonksiyonu çağrıldı");
            var response = new CustomerBasket
            {
                BuyerId = "test-user-info",
                Items = { new BasketItem { ProductId = "1", ProductName = "Kalem", Quantity = 5, Price = 100 } }
            };

            return Task.FromResult(response);
        }

        public override Task<CustomerBasket> UpdateBasket(CustomerBasket request, ServerCallContext context)
        {
           _logger.LogInformation("UpdateBasket rpc fonksiyonu çağrıldı");
            var response = new CustomerBasket
            {
                BuyerId = "test-user-info",
                Items = { new BasketItem { ProductId = "1", ProductName = "Kalem", Quantity = 5, Price = 100 } }
            };
            return Task.FromResult(request);
        }

        public override Task<DeleteBasketResponse> DeleteBasket(DeleteBasketRequest request, ServerCallContext context)
        {
            _logger.LogInformation("DeleteBasket rpc fonksiyonu çağrıldı");
            var response = new DeleteBasketResponse
            {
                Success = true

            };
            return Task.FromResult(response);
        }

        public override Task<CustomerBasket> AddItemToBasket(AddItemToBasketRequest request, ServerCallContext context)
        {
            _logger.LogInformation("AddItemToBasket rpc fonksiyonu çağrıldı");
            var response = new CustomerBasket
            {
                BuyerId = "test-user-info",
                Items = { new BasketItem { ProductId = "1", ProductName = "Kalem", Quantity = 5, Price = 100 } }
            };
            return Task.FromResult(response);   
        }
    }
}
