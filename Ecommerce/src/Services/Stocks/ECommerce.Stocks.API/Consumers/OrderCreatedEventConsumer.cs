using ECommerce.EventBus;
using MassTransit;

namespace ECommerce.Stocks.API.Consumers
{
    public class OrderCreatedEventConsumer(ILogger<OrderCreatedEventConsumer> logger) : IConsumer<OrderCreatedEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var command = context.Message.OrderCreateadCommand;
            bool isStockAvailable = checkStock(command.OrderItems);
            if (isStockAvailable)
            {
                //burada stok miktarı düşürme işlemi yapılacak (db'den)
                var eventCommand = new StockAvailableCommand(command.OrderId,command.CustomerId, command.CreditCardInfo, command.OrderItems.Sum(x => x.Price * x.Quantity));
                var @event = new StockAvailableEvent(eventCommand);

                await context.Publish(@event);
                logger.LogInformation($"Stock uygundur. Sipariş No: {command.OrderId}");

            }
            else
            {
                var eventCommand = new StockNotAvailableCommand(command.OrderId);
                var @event = new StockNotAvailable(eventCommand);
                await context.Publish(@event);
                logger.LogInformation($"Stock yetersizdir. Sipariş No: {command.OrderId}");
            }
        }

        private bool checkStock(IEnumerable<OrderItem> orderItems)
        {
            //foreach (var item in orderItems)
            //{
            //    item.
            //}
            return new Random().Next(0, 10) > 5;
        }
    }
}
