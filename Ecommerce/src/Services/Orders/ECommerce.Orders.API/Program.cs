using ECommerce.EventBus;
using ECommerce.Orders.API.Consumers;
using ECommerce.Orders.Infrastructure.Repositories;
using EventStore.Client;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<EventStoreRepoository>();
builder.Services.AddSingleton(new EventStoreClient(EventStoreClientSettings.Create("esdb://localhost:2113?tls=false")));

builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<OrderProductPriceDiscountConsumer>();
    configurator.AddConsumer<PaymentEventConsumer>();
    configurator.AddConsumer<StockFailedConsumer>();   

    configurator.UsingRabbitMq((context, config) =>
    {
        config.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        config.ReceiveEndpoint(EventQueues.OrderPriceDiscountQueue, e =>
        {
            e.ConfigureConsumer<OrderProductPriceDiscountConsumer>(context);
        });

        config.ConfigureEndpoints(context);

    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/orderCreate", async (IPublishEndpoint publisher, OrderCreateRequest request) =>
{
    //db işlemlerinin yapıldığını varsayalım....


    var orderItems = request.OrderItems.Select(x => new OrderItem(x.ProductId, x.Quantity, x.Price)).ToList();


    

    var orderId = new Random().Next(1000, 9000);
    var command = new OrderCreateadCommand(orderId, request.CustomerId, request.CreditCardInfo, orderItems);
    var @event = new OrderCreatedEvent(command);

    await publisher.Publish(@event);


});


app.Run();

public record OrderCreateRequest(string CustomerId, string CreditCardInfo, List<OrderItemInRequest> OrderItems);
public record OrderItemInRequest(Guid ProductId, int Quantity, decimal Price);