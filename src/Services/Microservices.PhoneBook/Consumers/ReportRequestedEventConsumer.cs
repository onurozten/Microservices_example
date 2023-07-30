using MassTransit;
using Microservices.PhoneBook.Data;
using Microservices.PhoneBook.Helper;
using Microservices.Shared.Messages;

namespace Microservices.PhoneBook.Consumers
{

    public class ReportRequestedEventConsumer : IConsumer<ReportRequestedEvent>
    {
        private readonly AppDbContext _dbContext;
        private readonly IPublishEndpoint _publishEndpoint;

        public ReportRequestedEventConsumer(AppDbContext dbContext, IPublishEndpoint publishEndpoint)
        {
            _dbContext = dbContext;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<ReportRequestedEvent> context)
        {
            var location = context.Message.Location;

            PhoneBookHelper.CountLocationAndPhones(_dbContext, location, out var count, out var phoneCount);

            await _publishEndpoint.Publish(new ReportFinishedEvent
            { 
                ReportId = context.Message.ReportId,
                Location = location,
                LocationCount = count,
                PhoneCount = phoneCount,
            });

        }
    }
}
