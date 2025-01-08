using ECommerce.Catalog.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Infrastructure.EventHandlers
{
    public class ProductPriceDiscountedDomainEventHandler : INotificationHandler<ProductDiscountedDomainEvent>
    {
        public Task Handle(ProductDiscountedDomainEvent notification, CancellationToken cancellationToken)
        {
            //TODO 1: Burada, integration event (ProductDiscountedIntegrationEvent) oluşturulacak ve RabbitMQ'ya gönderilecek.

            throw new NotImplementedException();
        }
    }
}
