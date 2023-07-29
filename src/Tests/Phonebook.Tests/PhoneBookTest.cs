using Microservices.PhoneBook.Data;
using Microservices.PhoneBook.Helper;
using Microservices.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace Phonebook.Tests
{
    [TestClass]
    public class PhoneBookTest
    {
        private readonly AppDbContext _dbContext;

        public PhoneBookTest()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                            .UseInMemoryDatabase(databaseName: "InMemoryPhonebookDb")
                            .Options;

            _dbContext = new AppDbContext(options);
        }

        [TestMethod]
        public void people_location_and_phonecount_test()
        {
            var location = "İSTANBUL";

            PhoneBookHelper.CountLocationAndPhones(_dbContext, location, out var peopleCount, out var peopleCountWithPhone);
            Assert.AreEqual(0, peopleCount);

            _dbContext.People.Add(new Person
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Onur",
                Surname = "Özten",
                Company = "Company",
                ContactInfos = new List<ContactInfo>
                {
                    new ContactInfo
                    {
                        ContactType = ContactType.Location,
                        ContactContent = "İSTANBUL"
                    }
                }
            });

            _dbContext.SaveChanges();
            PhoneBookHelper.CountLocationAndPhones(_dbContext, location, out peopleCount, out peopleCountWithPhone);
            Assert.AreEqual(1, peopleCount);
            Assert.AreEqual(0, peopleCountWithPhone);

            _dbContext.People.Add(new Person
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Cemal",
                Surname = "Sagıp",
                Company = "Company",
                ContactInfos = new List<ContactInfo>
                {
                    new ContactInfo
                    {
                        ContactType = ContactType.Location,
                        ContactContent = "İSTANBUL"
                    },
                    new ContactInfo
                    {
                        ContactType = ContactType.Phone,
                        ContactContent = "1234567896"
                    }
                }
            });

            _dbContext.SaveChanges();
            PhoneBookHelper.CountLocationAndPhones(_dbContext, location, out peopleCount, out peopleCountWithPhone);
            Assert.AreEqual(2, peopleCount);
            Assert.AreEqual(1, peopleCountWithPhone);
        }
    }
}