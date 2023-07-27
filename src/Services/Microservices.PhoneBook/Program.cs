using Microservices.PhoneBook.Data;
using Microservices.PhoneBook.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

///////////////////////

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPersonService, PersonService>();




//////////////////////

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var appDbContext = serviceProvider.GetRequiredService<AppDbContext>();
    appDbContext.Database.Migrate();

    if (!appDbContext.People.Any())
    {
        appDbContext.People.Add(new Person
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Eren",
            Surname = "Özten",
            Company = "Contoso University"
        });

        appDbContext.People.Add(new Person
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Kerim",
            Surname = "Balkan",
            Company = "Anadolu University"
        });

        appDbContext.People.Add(new Person
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Sevim",
            Surname = "Güler",
            Company = "Software Company"
        });

        appDbContext.People.Add(new Person
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Onur",
            Surname = "Özten",
            Company = "Another Software Company"
        });

        appDbContext.SaveChanges();
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
