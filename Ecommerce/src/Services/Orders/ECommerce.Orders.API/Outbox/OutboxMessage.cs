namespace ECommerce.Orders.API.Outbox
{
    public class OutboxMessage
    {
        public Guid Id { get; set; }
        public string AggregateType { get; set; }
        public int AggregateId { get; set; }
        public string  EventType { get; set; }
        public string Payload { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Processed { get; set; }


    }
}
