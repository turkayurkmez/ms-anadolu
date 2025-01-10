namespace ECommerce.EventBus
{
    public record OrderCreatedEvent(OrderCreateadCommand OrderCreateadCommand): IntegrationEvent;

    public record OrderCreateadCommand(int OrderId, string CustomerId, string? CreditCardInfo, IEnumerable<OrderItem> OrderItems);
    public record OrderItem(Guid ProductId, int Quantity, decimal Price);

}
