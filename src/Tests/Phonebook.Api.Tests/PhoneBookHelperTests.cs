using Microsoft.EntityFrameworkCore;
using Microservices.PhoneBook.Data;
using Microservices.PhoneBook.Helper;
using Microservices.Shared.Enums;
using FluentAssertions;

namespace Phonebook.Tests
{
    public class PhoneBookHelperTests
    {
        [Fact]
        public void CountLocationAndPhones_Should_Return_Correct_Counts()
        {
            // arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                   .UseInMemoryDatabase(databaseName: "InMemoryPhonebookDb").Options;

            var _dbContext = new AppDbContext(options);

            var location = "İSTANBUL";

            PhoneBookHelper.CountLocationAndPhones(_dbContext, location, out var peopleCount, out var peopleCountWithPhone);
            peopleCount.Should().Be(0);

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

            peopleCount.Should().Be(1);
            peopleCountWithPhone.Should().Be(0);

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

            peopleCount.Should().Be(2);
            peopleCountWithPhone.Should().Be(1);

            //Assert.Equal(2, peopleCount);
            //Assert.Equal(1, peopleCountWithPhone);

        }
    }
}
    
