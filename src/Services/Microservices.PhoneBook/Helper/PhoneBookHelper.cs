using Microservices.PhoneBook.Data;
using Microservices.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace Microservices.PhoneBook.Helper
{
    public static class PhoneBookHelper 
    {
        public static void CountLocationAndPhones(AppDbContext dbContext, string location, out int count, out int phoneCount)
        {
            var peopleAtLocation = (from u in dbContext.People
                                         join c in dbContext.ContactInfos
                                         on u.Id equals c.PersonId
                                         where c.ContactContent == location
                                         select u)
                                            .Include(x => x.ContactInfos)
                                            .ToList();

            count = peopleAtLocation.Count();
            phoneCount = 0;
            foreach (var item in peopleAtLocation)
            {
                foreach (var c in item.ContactInfos)
                {
                    if (c.ContactTypeId == (int)ContactType.Phone)
                        phoneCount++;
                }
            }
        }
    }
}
