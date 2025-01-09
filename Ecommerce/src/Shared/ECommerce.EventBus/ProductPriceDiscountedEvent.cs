using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.EventBus
{
    public record ProductPriceDiscountedEvent(Guid ProductId, decimal OldPrice, decimal NewPrice) : IntegrationEvent;
    
}
