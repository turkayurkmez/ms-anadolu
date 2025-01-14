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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JwtSettings:Audience"],                        
                        ValidateLifetime = true,
                        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))

                    };
                });


builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
connectionString = connectionString.Replace("[PASS]", builder.Configuration.GetSection("DefaultPassword").Value).Replace("[HOST]", builder.Configuration.GetSection("DefaultHostName").Value);

//RabbitMQ ile tokalaşmak için konfigürasyon API üzerinden yapılır.
builder.Services.AddMassTransit(configurator =>
{

    //configurator.AddEntityFrameworkOutbox<CatalogDbContext>(o =>
    //{
    //    o.QueryDelay = TimeSpan.FromSeconds(1);
    //    o.UseSqlServer();
    //    o.UseBusOutbox();
    //});
    configurator.UsingRabbitMq((context, config) =>
    {
        config.Host("rabbit-mq", "/", h =>
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

app.Logger.LogWarning($"Bağlantı adresi: {connectionString}");



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

await DatabaseInitializer.CreateAndSeedDatabaseAsync(app);
app.UseHttpsRedirection();
app.UseProductsEndPoints();


app.Run();

