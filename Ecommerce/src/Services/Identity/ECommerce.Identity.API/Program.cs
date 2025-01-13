using ECommerce.Identity.API.Extensions;
using ECommerce.Identity.Application.Contracts;
using ECommerce.Identity.Application.Services;
using ECommerce.Identity.Infrastructure.Extensions;
using ECommerce.Identity.Infrastructure.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddJwtSettings(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/login", async (IUserService userService, LoginRequest request) =>
{
    var result = await userService.Login(request.Email, request.Password);

    return Results.Ok(new {token = result});
});

app.MapPost("/register", async (IUserService userService, RegisterRequest request) =>
{
    var result = await userService.RegisterAsync(request.Email, request.Password, request.UserName);
    return Results.Ok(new { message = $"{result.ToString()} oluşturuldu" });
});



app.Run();



public record LoginRequest(string Email, string Password);
public record RegisterRequest(string Email, string Password, string UserName);
