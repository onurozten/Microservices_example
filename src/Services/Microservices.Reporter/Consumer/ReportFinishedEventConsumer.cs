using MassTransit;
using Microservices.Reporter.Model;
using Microservices.Shared.Messages;
using Microservices.Shared.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Microservices.Reporter.Consumers
{

    public class ReportFinishedEventConsumer : IConsumer<ReportFinishedEvent>
    {

        private readonly IMongoCollection<Report> _reportCollection;

        public ReportFinishedEventConsumer(IOptions<DatabaseSettings> databaseSettings)
        {
            var client = new MongoClient(databaseSettings.Value.ConnectionString);
            var database = client.GetDatabase(databaseSettings.Value.DatabaseName);
            _reportCollection = database.GetCollection<Report>(databaseSettings.Value.CollectionName);
        }

        public async Task Consume(ConsumeContext<ReportFinishedEvent> context)
        {
            var newReport = new Report{ 
                FinishedAd = DateTime.Now,
                Location = context.Message.Location,
                LocationCount = context.Message.LocationCount,
                PhoneCount = context.Message.PhoneCount,
                StaredAt = context.Message.StaredAt
            };

            await _reportCollection.InsertOneAsync(newReport);
        }
    }
}
