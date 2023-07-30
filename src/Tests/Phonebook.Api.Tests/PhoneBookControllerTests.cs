using Microservices.PhoneBook.Controllers;
using Microservices.PhoneBook.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace PhoneBook.Api.Tests;

public class PhoneBookControllerTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task DeleteDetailById_ReturnsBadRequest_WhenGivenIdIsInvalid(int id)
    {
        // arrange
        IPersonService personService = null!;
        var controller = new PhoneBookController(personService);

        // act
        var result = await controller.DeleteDetailById(id);

        // assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task DeleteDetailById_ReturnsOk_WhenGivenIdIsValid()
    {
        // arrange
        const int id = 1;

        var mockPersonService = new Mock<IPersonService>();
        mockPersonService
            .Setup(t => t.DeleteDetailById(id))
            .Returns(() => Task.CompletedTask)
            .Verifiable();

        var controller = new PhoneBookController(mockPersonService.Object);

        // act
        var result = await controller.DeleteDetailById(id);

        // assert
        Assert.IsType<OkResult>(result);
        mockPersonService.Verify(t => t.DeleteDetailById(It.Is<int>(ti => ti == id)), Times.Once);
    }
}