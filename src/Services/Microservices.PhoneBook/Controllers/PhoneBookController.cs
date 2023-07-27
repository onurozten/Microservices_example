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
        public async Task<IActionResult> Index()
        {
            var peoples = await _personService.GetAll();
            return Ok(peoples);
        }
    }
}
