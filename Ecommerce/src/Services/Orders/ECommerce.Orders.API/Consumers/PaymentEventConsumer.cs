using ECommerce.EventBus;
using MassTransit;

namespace ECommerce.Orders.API.Consumers
{
    public class PaymentEventConsumer(ILogger<PaymentEventConsumer> logger)
                                                    : IConsumer<PaymentSuccessEvent>,
                                                      IConsumer<PaymentFailedEvent>
    {
        public Task Consume(ConsumeContext<PaymentSuccessEvent> context)
        {
           var command = context.Message.Command;
            logger.LogInformation($"Ödeme başarılı. Sipariş No: {command.OrderId}");
            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<PaymentFailedEvent> context)
        {
            var command = context.Message.Command;

            //unutmayın: db'de siparişi iptal (failed) etmek için flag ayarlaması yapılmalıdır.
            logger.LogInformation($"Ödeme başarısız. Sipariş No: {command.OrderId}, sebebi: {command.Reason}");
            return Task.CompletedTask;

        }
    }
}
