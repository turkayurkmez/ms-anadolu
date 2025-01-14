using ECommerce.Payment.API.Consumers;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<StockAvailableEventConsumer>(c => {

        c.UseMessageRetry(retry =>
        {
            retry.Intervals(

                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(15),
                TimeSpan.FromSeconds(30)
            );

            //retry.Exponential()

            retry.Handle<TimeoutException>();



            //her retry için loglama oluştur:
            //retry.OnRetry((context, exception, retryCount) =>
            //{
            //    Console.WriteLine($"Retry Count: {retryCount}");
            //});


        });

        c.UseCircuitBreaker(cbc =>
        {
            cbc.TrackingPeriod = TimeSpan.FromMinutes(1);
            cbc.TripThreshold = 15; //1 dakika içinde 15 hata olursa
            cbc.ActiveThreshold = 10; //10 aktif istek varsa
            cbc.ResetInterval = TimeSpan.FromMinutes(5); //5 dakika sonra resetle (Circuit açıkken ne kadar bekleyecek?)
        });
    
    });

    configurator.UsingRabbitMq((context, config) =>
    {
        config.Host("rabbit-mq", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        config.UseMessageRetry(retry =>
        {
            retry.Exponential(
                retryLimit: 5,
                minInterval: TimeSpan.FromSeconds(1),
                maxInterval: TimeSpan.FromSeconds(64),
                intervalDelta: TimeSpan.FromSeconds(2)
                );
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



app.Run();

