using MassTransit;
using Microservices.Web.Models;
using Microservices.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        // Default port 5672
        cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });
    });
});


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection("ServiceApiSettings"));
var serviceApiSetttings = builder.Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

builder.Services.AddHttpClient<IPersonService, PersonService>(opt =>
{
    opt.BaseAddress = new Uri($"{serviceApiSetttings.GetawayBaseUri}/{serviceApiSetttings.PhoneBook.Path}");
});
builder.Services.AddHttpClient<IReportService, ReportService>(opt =>
{
    opt.BaseAddress = new Uri($"{serviceApiSetttings.GetawayBaseUri}/{serviceApiSetttings.Reporter.Path}");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
