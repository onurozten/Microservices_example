using Microservices.Web.Models;

namespace Microservices.Web.Services
{
    public class PersonService : IPersonService
    {
        private readonly HttpClient _httpClient;

        public PersonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PersonVm>> GetAll()
        {
            var people = await _httpClient.GetFromJsonAsync<IEnumerable<PersonVm>>("phonebook");

            return people;
        }
    }
}
