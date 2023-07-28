using MassTransit;
using Microservices.Reporter.Consumers;
using Microservices.Reporter.Model;
using Microservices.Reporter.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));


builder.Services.AddScoped<IReportService, ReportService>();


builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ReportFinishedEventConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        var a = builder.Configuration["RabbitMQUrl"];
        // Default port 5672
        cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        cfg.ReceiveEndpoint("report-finished-event-report-service", e =>
        {
            e.ConfigureConsumer<ReportFinishedEventConsumer>(context);
        });

    });
});

// Add services to the container.

builder.Services.AddControllers(); 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
