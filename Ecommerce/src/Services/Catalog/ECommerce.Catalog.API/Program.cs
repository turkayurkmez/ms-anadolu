using ECommerce.Catalog.API;
using ECommerce.Catalog.Application.Contracts;
using ECommerce.Catalog.Application.Features.Products.Commands.DiscountPrice;
using ECommerce.Catalog.Infrastructure.Extensions;

using ECommerce.Catalog.Infrastructure.Persistence;
using ECommerce.Catalog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using ECommerce.EventBus;
using MassTransit.Transports.Fabric;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();


builder.Services.AddMassTransit(configurator =>
{
    configurator.UsingRabbitMq((context, config) =>
    {
        config.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        config.Publish<ProductPriceDiscountedEvent>(x =>
           x.ExchangeType = "fanout"
        );

    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


await DatabaseInitializer.CreateAndSeedDatabaseAsync(app);
app.UseHttpsRedirection();
app.UseProductsEndPoints();


app.Run();

