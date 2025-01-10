using ECommerce.EventBus;
using MassTransit;

namespace ECommerce.Payment.API.Consumers
{
    public class StockAvailableEventConsumer(ILogger<StockAvailableEventConsumer> logger) : IConsumer<StockAvailableEvent>
    {
        public async Task Consume(ConsumeContext<StockAvailableEvent> context)
        {
            var command = context.Message.Command;
            
            var paymentIsSuccess = new Random().Next(0, 10) > 5;

            if (paymentIsSuccess)
            {
                var paymentSucceededCommand = new PaymentSucceededCommand(command.OrderId, command.CustomerId);
                var @event = new PaymentSuccessEvent(paymentSucceededCommand);
                await context.Publish<PaymentSuccessEvent>(@event);
                logger.LogInformation($"Ödeme başarılı. Sipariş No: {command.OrderId}");
            }
            else
            {
                var paymentFailedCommand = new PaymentFailedCommand(command.OrderId, command.CustomerId, "Ödeme başarısız");
                var @event = new PaymentFailedEvent(paymentFailedCommand);
                await context.Publish(@event);
                logger.LogInformation($"Ödeme başarısız. Sipariş No: {command.OrderId}");
            }           






        }
    }
}
