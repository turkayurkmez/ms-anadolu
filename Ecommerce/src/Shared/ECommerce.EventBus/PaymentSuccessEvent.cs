using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.EventBus
{
    public  record PaymentSuccessEvent(PaymentSucceededCommand Command) : IntegrationEvent;

    public record PaymentSucceededCommand(int OrderId, string CustomerId);

    public record PaymentFailedEvent(PaymentFailedCommand Command) : IntegrationEvent;

    public record PaymentFailedCommand(int OrderId, string CustomerId, string Reason);



}
