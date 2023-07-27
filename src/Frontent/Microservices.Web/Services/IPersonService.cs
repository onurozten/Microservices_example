using Microservices.Web.Models;

namespace Microservices.Web.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonVm>> GetAll();
        Task<PersonVm> GetById(string id);
        Task DeleteById(string id);
        Task Create(PersonCreateDto createVm);
        Task CreateDetail(ContactCreateModel createModel);
        Task DeleteDetailById(int id);
    }
}
