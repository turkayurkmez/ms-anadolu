using ECommerce.Basket.API;
using ECommerce.Basket.API.Consumers;
using ECommerce.Basket.API.Services;
using ECommerce.EventBus;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<BasketProductPriceDiscountConsumer>();
    configurator.UsingRabbitMq((context, config) =>
    {
        config.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

       config.ReceiveEndpoint("basket-price-discount-event", e =>
       {
           e.ConfigureConsumer<BasketProductPriceDiscountConsumer>(context);
       });

    });
});


builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<CustomBasketService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
