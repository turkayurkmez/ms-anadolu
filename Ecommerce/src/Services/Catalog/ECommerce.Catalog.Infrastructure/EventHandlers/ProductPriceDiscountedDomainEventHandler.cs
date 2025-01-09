using ECommerce.Catalog.Domain.Events;
using ECommerce.EventBus;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Infrastructure.EventHandlers
{
    public class ProductPriceDiscountedDomainEventHandler : INotificationHandler<ProductDiscountedDomainEvent>
    {
        private readonly ILogger<ProductPriceDiscountedDomainEventHandler> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public ProductPriceDiscountedDomainEventHandler(ILogger<ProductPriceDiscountedDomainEventHandler> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }
        public Task Handle(ProductDiscountedDomainEvent notification, CancellationToken cancellationToken)
        {
            //TODO 1: Burada, integration event (ProductDiscountedIntegrationEvent) oluşturulacak ve RabbitMQ'ya gönderilecek.

            _logger.LogInformation($"Ürün fiyatı indirildi! ProductId: {notification.ProductId}, OldPrice: {notification.OldPrice}, NewPrice: {notification.NewPrice}");

            var @event = new ProductPriceDiscountedEvent(notification.ProductId, notification.OldPrice, notification.NewPrice);

            _publishEndpoint.Publish(@event);



            return Task.CompletedTask;
        }
    }
}
