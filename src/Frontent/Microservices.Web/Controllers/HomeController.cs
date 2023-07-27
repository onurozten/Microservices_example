using Microservices.Web.Models;
using Microservices.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Microservices.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPersonService _personService;

        public HomeController(IPersonService personService)
        {
            _personService = personService;
        }

        public async Task<IActionResult> Index()
        {
            var people = await _personService.GetAll();
            return View(people);
        }


    }
}