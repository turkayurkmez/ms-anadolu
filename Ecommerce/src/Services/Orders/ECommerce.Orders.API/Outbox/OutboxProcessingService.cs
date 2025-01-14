
namespace ECommerce.Orders.API.Outbox
{
    public class OutboxProcessingService : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //Bu metotta, Outbox tablosundan alınan, işlenmemiş mesajlar tekrar işlenmelidir.

            throw new NotImplementedException();
        }
    }
}
