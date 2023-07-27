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

        public async Task<PersonVm> GetById(string id)
        {
            var res = await _httpClient.GetStringAsync($"phonebook/getbyid/{id}");
            var people = await _httpClient.GetFromJsonAsync<PersonVm>($"phonebook/getbyid/{id}");

            return people;
        }

        public async Task Create(PersonCreateDto createVm)
        {
            var response = await _httpClient.PostAsJsonAsync("phonebook", createVm);

            /*
            if (response.IsSuccessStatusCode)
            {

            }
            */
        }

        public async Task CreateDetail(ContactCreateModel createModel)
        {
            await _httpClient.PostAsJsonAsync("phonebook/createdetail", createModel);
        }

        public async Task DeleteById(string id)
        {
            var result = await _httpClient.DeleteAsync($"phonebook/{id}");
        }

        public async Task DeleteDetailById(int id)
        {
            var result = await _httpClient.DeleteAsync($"phonebook/deletedetailbyid/{id}");
        }

    }
}
