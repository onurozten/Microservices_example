using Microservices.Web.Models;
using Microservices.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection("ServiceApiSettings"));

builder.Services.AddHttpClient<IPersonService, PersonService>(opt =>
{
    var serviceApiSetttings = builder.Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

    opt.BaseAddress = new Uri($"{serviceApiSetttings.GetawayBaseUri}/{serviceApiSetttings.PhoneBook.Path}");
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
