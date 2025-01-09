using ECommerce.EventBus;
using MassTransit;

namespace ECommerce.Orders.API.Consumers
{
    public class OrderProductPriceDiscountConsumer (ILogger<OrderProductPriceDiscountConsumer> logger) : IConsumer<ProductPriceDiscountedEvent>
    {
         
        public Task Consume(ConsumeContext<ProductPriceDiscountedEvent> context)
        {
            var message = context.Message;
            //yeni ürün fiyatını logla:

            logger.LogInformation($"Ürün fiyatı indirildi! ProductId: {message.ProductId}, OldPrice: {message.OldPrice}, NewPrice: {message.NewPrice}");

            return Task.CompletedTask;
        }
    }
}
