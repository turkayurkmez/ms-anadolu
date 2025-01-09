using ECommerce.Catalog.Domain.Events;
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

        public ProductPriceDiscountedDomainEventHandler(ILogger<ProductPriceDiscountedDomainEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(ProductDiscountedDomainEvent notification, CancellationToken cancellationToken)
        {
            //TODO 1: Burada, integration event (ProductDiscountedIntegrationEvent) oluşturulacak ve RabbitMQ'ya gönderilecek.

            _logger.LogInformation($"Ürün fiyatı indirildi! ProductId: {notification.ProductId}, OldPrice: {notification.OldPrice}, NewPrice: {notification.NewPrice}");

            return Task.CompletedTask;
        }
    }
}
