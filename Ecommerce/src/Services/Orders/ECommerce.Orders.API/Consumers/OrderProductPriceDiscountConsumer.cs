using ECommerce.EventBus;
using ECommerce.Orders.Infrastructure.Repositories;
using MassTransit;

namespace ECommerce.Orders.API.Consumers
{
    public class OrderProductPriceDiscountConsumer (ILogger<OrderProductPriceDiscountConsumer> logger, EventStoreRepoository eventStoreRepoository) : IConsumer<ProductPriceDiscountedEvent>
    {
         
        public async Task Consume(ConsumeContext<ProductPriceDiscountedEvent> context)
        {
            var message = context.Message;
            //yeni ürün fiyatını logla:

            //burada, EventStore üzerine kayıt girilmelidir.
            var streamName = $"ProductDiscount-{message.ProductId}";
            await eventStoreRepoository.AppendEventAsync(streamName, message);

            logger.LogInformation($"Ürün fiyatı indirildi! ProductId: {message.ProductId}, OldPrice: {message.OldPrice}, NewPrice: {message.NewPrice}");

            //return Task.CompletedTask;
        }
    }
}
