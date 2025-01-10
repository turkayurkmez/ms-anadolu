using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.EventBus
{
    public  record StockAvailableEvent(StockAvailableCommand Command):IntegrationEvent;

    //Not: Stock, ödeme başarısız olduğu takdirde, stoğa geri eklenmelidir. Bu durumu geliştirmedik. Ama isteseydik, StockAvailable içinde düşülen stoklar da tutulmalıydı.
    public record StockAvailableCommand(int OrderId, string CustomerId, string CreditCardInfo, decimal? TotalPrice);

    public record StockNotAvailable(StockNotAvailableCommand Command): IntegrationEvent;

    public record StockNotAvailableCommand(int OrderId);



}
