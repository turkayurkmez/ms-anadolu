using ECommerce.Shared.Library.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Catalog.Domain.Events
{
    //Dikkat bu bir Domain Event! Yani, sadece nesnel bir olay. Rabbit MQ'ya gönderilmeyecek!
    public class ProductDiscountedDomainEvent: DomainEvent, INotification
    {
        public Guid ProductId { get; private set; }
        public decimal OldPrice { get; private set; }
        public decimal NewPrice { get; private set; }
        public ProductDiscountedDomainEvent(Guid productId, decimal oldPrice, decimal newPrice)
        {
            ProductId = productId;
            OldPrice = oldPrice;
            NewPrice = newPrice;
        }

    }
}
