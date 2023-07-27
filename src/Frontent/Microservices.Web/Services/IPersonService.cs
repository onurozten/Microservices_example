using Microservices.Web.Models;

namespace Microservices.Web.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonVm>> GetAll();
    }
}
