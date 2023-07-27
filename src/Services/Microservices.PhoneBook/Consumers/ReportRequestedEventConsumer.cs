using MassTransit;
using MassTransit.Transports;
using Microservices.PhoneBook.Data;
using Microservices.Shared.Enums;
using Microservices.Shared.Messages;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
            var start = DateTime.Now;
            var location = context.Message.Location;

            var peopleAtLocation = await (from u in _dbContext.People
                                          join c in _dbContext.ContactInfos
                                          on u.Id equals c.PersonId
                                          where c.ContactContent == location
                                          select u)
                                            .Include(x => x.ContactInfos)
                                            .ToListAsync();

            var phoneCount = 0;
            foreach (var item in peopleAtLocation)
            {
                foreach (var c in item.ContactInfos)
                {
                    if (c.ContactTypeId == (int)ContactType.Phone)
                        phoneCount++;
                }
            }

            var result = new
            {
                located = peopleAtLocation,
                locatedPhoneNumber = phoneCount
            };

            var end = DateTime.Now;
            await _publishEndpoint.Publish<ReportFinishedEvent>(new ReportFinishedEvent
            { 
                LocationCount = peopleAtLocation.Count(),
                PhoneCount = phoneCount,
                StaredAt = start,
                FinishedAd = end,
                Location = location 
            });

        }
    }
}
