using Microservices.PhoneBook.Dtos;

namespace Microservices.PhoneBook.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonDto>> GetAll();
        Task<PersonDto> GetById(string id);
        Task DeleteById(string id);
        Task Create(PersonCreateDto createDto);
        Task CreateDetail(ContactCreateDto createDto);
        Task DeleteDetailById(int id);
    }
}
