using MassTransit;
using Microservices.Reporter.Model;
using Microservices.Shared.Enums;
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
            var reportToUpdate = await _reportCollection.Find(x => x.Id == context.Message.ReportId).FirstOrDefaultAsync();
            reportToUpdate.FinishedAd = DateTime.Now;
            reportToUpdate.Location = context.Message.Location;
            reportToUpdate.LocationCount = context.Message.LocationCount;
            reportToUpdate.PhoneCount = context.Message.PhoneCount;
            reportToUpdate.ReportState = (int)ReportState.Completed;

            await _reportCollection.FindOneAndReplaceAsync(x => x.Id == reportToUpdate.Id, reportToUpdate);
        }
    }
}
