using ECommerce.EventBus;
using MassTransit;

namespace ECommerce.Basket.API.Consumers
{
    public class BasketProductPriceDiscountConsumer : IConsumer<ProductPriceDiscountedEvent>
    {

        private ILogger<BasketProductPriceDiscountConsumer> _logger;

        public BasketProductPriceDiscountConsumer(ILogger<BasketProductPriceDiscountConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<ProductPriceDiscountedEvent> context)
        {
            var message = context.Message;
            _logger.LogInformation($"Ürün fiyatı değişti! {message.ProductId} isimli ürünün fiyatı sepetlerde {message.NewPrice} olarak güncellendi!  ");
            return Task.CompletedTask;

        }
    }
}
