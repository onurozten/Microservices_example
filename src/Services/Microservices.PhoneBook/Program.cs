using FluentValidation;
using MassTransit;
using Microservices.PhoneBook.Consumers;
using Microservices.PhoneBook.Data;
using Microservices.PhoneBook.Dtos;
using Microservices.PhoneBook.Services;
using Microservices.Shared.Enums;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ReportRequestedEventConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        var a = builder.Configuration["RabbitMQUrl"];
        // Default port 5672
        cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        cfg.ReceiveEndpoint("report-requested-event-report-service", e =>
        {
            e.ConfigureConsumer<ReportRequestedEventConsumer>(context);
        });

    });
});

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddValidatorsFromAssemblyContaining<ContactCreateDtoValidator>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();

    if (!dbContext.People.Any())
        SeedData(dbContext);
}

void SeedData(AppDbContext dbContext)
{
    dbContext.People.Add(new Person
    {
        Id = Guid.NewGuid().ToString(),
        Name = "Eren",
        Surname = "Özten",
        Company = "Contoso University",
        ContactInfos = new List<ContactInfo>
            {
                new ContactInfo
                {
                    ContactType = ContactType.Phone,
                    ContactContent = "5129898569"
                },
                new ContactInfo
                {
                    ContactType = ContactType.Email,
                    ContactContent = "eren@eren.com"
                },
                new ContactInfo
                {
                    ContactType = ContactType.Location,
                    ContactContent = "İSTANBUL"
                }
            }
    });

    dbContext.People.Add(new Person
    {
        Id = Guid.NewGuid().ToString(),
        Name = "Kerim",
        Surname = "Balkan",
        Company = "Anadolu University",
        ContactInfos = new List<ContactInfo>
            {
                new ContactInfo
                {
                    ContactType = ContactType.Email,
                    ContactContent = "kerim@kerim.com"
                },
                new ContactInfo
                {
                    ContactType = ContactType.Location,
                    ContactContent = "İSTANBUL"
                }
            }
    });

    dbContext.People.Add(new Person
    {
        Id = Guid.NewGuid().ToString(),
        Name = "Sevim",
        Surname = "Güler",
        Company = "Software Company",
        ContactInfos = new List<ContactInfo>
            {
                new ContactInfo
                {
                    ContactType = ContactType.Phone,
                    ContactContent = "5965487236"
                },
                new ContactInfo
                {
                    ContactType = ContactType.Email,
                    ContactContent = "sevim@sevim.com"
                },
                new ContactInfo
                {
                    ContactType = ContactType.Location,
                    ContactContent = "ANKARA"
                }
            }
    });

    dbContext.People.Add(new Person
    {
        Id = Guid.NewGuid().ToString(),
        Name = "Onur",
        Surname = "Özten",
        Company = "Another Software Company"
    });

    dbContext.SaveChanges();
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
