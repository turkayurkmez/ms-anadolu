using Microsoft.AspNetCore.Authentication.BearerToken;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddReverseProxy()
                .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

//builder.Services.AddAuthentication(BearerTokenDefaults.AuthenticationScheme).AddBearerToken();
//builder.Services.AddAuthorization(o => o.AddPolicy("product-api", p => p.RequireClaim("Name")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/login", () => {
    //Identity service'e request gönderilerek token alınacak

    return Results.SignIn(new ClaimsPrincipal(
                            new ClaimsIdentity([
                                new Claim(ClaimTypes.Name, "client"),
                                ], BearerTokenDefaults.AuthenticationScheme)
                            ),authenticationScheme: BearerTokenDefaults.AuthenticationScheme
                          );


});

app.UseHttpsRedirection();
//app.UseAuthentication();
//app.UseAuthorization();
app.MapReverseProxy();

app.Run();

