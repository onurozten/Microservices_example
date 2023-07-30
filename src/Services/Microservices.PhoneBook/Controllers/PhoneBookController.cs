using Microservices.PhoneBook.Dtos;
using Microservices.PhoneBook.Services;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.PhoneBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneBookController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PhoneBookController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var peoples = await _personService.GetAll();
            return Ok(peoples);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var person = await _personService.GetById(id);
            return Ok(person);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(string id)
        {
            await _personService.DeleteById(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonCreateDto createDto)
        {
            await _personService.Create(createDto);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateDetail(ContactCreateDto createDto)
        {
            await _personService.CreateDetail(createDto);
            return Ok();
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteDetailById(int id)
        {
            if (id == default || id < 0)
                return BadRequest();

            await _personService.DeleteDetailById(id);
            return Ok();
        }

    }
}
