
using Bogus;
//using FluentAssertions;
using FluentValidation.TestHelper;
using Microservices.PhoneBook.Dtos;

namespace PhoneBook.Api.Tests.Validator;

public class ContactCreateDtoValidatorTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void ShouldHaveError_When_ContactContentIsLeftBlank(string contactContent)
    {
        var validator = new ContactCreateDtoValidator();
        var validModel = MockContentFactory.CreateValidCreateContactDto().Generate();

        validModel.ContactContent = contactContent;

        var result = validator.TestValidate(validModel);

        result.ShouldHaveValidationErrorFor(t => t.ContactContent);
    }

    [Fact]
    public void ShouldHaveError_When_IdIsEmpty()
    {
        var validator = new ContactCreateDtoValidator();
        var validModel = MockContentFactory.CreateValidCreateContactDto().Generate();

        validModel.Id = 0;

        var result = validator.TestValidate(validModel);

        result
            .ShouldHaveValidationErrorFor(t => t.Id)
            .WithErrorMessage(ErrorMessages.Contact.EmptyId);
    }

    [Fact]
    public void ShouldHaveError_When_IdIsNegative()
    {
        var validator = new ContactCreateDtoValidator();
        var validModel = MockContentFactory.CreateValidCreateContactDto().Generate();

        validModel.Id = -100;

        var result = validator.TestValidate(validModel);

        result
            .ShouldHaveValidationErrorFor(t => t.Id)
            .WithErrorMessage(ErrorMessages.Contact.NegativeId);
    }
}

public static class MockContentFactory
{
    public static Faker<ContactCreateDto> CreateValidCreateContactDto()
    {
        Faker<ContactCreateDto> faker = new Faker<ContactCreateDto>();

        faker.CustomInstantiator(f => new ContactCreateDto()
        {
            Id = f.Random.Int(0, 100),
            ContactTypeId = f.Random.Int(0, 10),
            ContactContent = f.Lorem.Paragraph(2),
            PersonId = f.Random.Guid().ToString()
        });

        return faker;
    }
}
