using ECommerce.EventBus;
using MassTransit;

namespace ECommerce.Orders.API.Consumers
{
    public class StockFailedConsumer(ILogger<StockFailedConsumer> logger) : IConsumer<StockNotAvailable>
    {
        public Task Consume(ConsumeContext<StockNotAvailable> context)
        {
            var command = context.Message.Command;
            logger.LogInformation($"Stok yetersizdir. Sipariş No: {command.OrderId}, siparişi Failed olarak belirlendi");
            return Task.CompletedTask;
        }
    }
}
