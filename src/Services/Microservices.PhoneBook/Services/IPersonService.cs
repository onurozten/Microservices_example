using Microservices.PhoneBook.Dtos;

namespace Microservices.PhoneBook.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonDto>> GetAll();
    }
}
