using MassTransit;
using Microservices.Shared.Messages;
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

        public async Task<IActionResult> Delete(string id)
        {
            await _personService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(string id)
        {
            var people = await _personService.GetById(id);
            return View(people);
        }

        public async Task<IActionResult> Create(PersonCreateDto createVm)
        {
            await _personService.Create(createVm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CreateDetail(ContactCreateModel createDto)
        {
            await _personService.CreateDetail(createDto);
            return RedirectToAction(nameof(Detail), new {id = createDto.PersonId});
        }


        public async Task<IActionResult> DeleteDetail(int id, string personId)
        {
            await _personService.DeleteDetailById(id);
            return RedirectToAction(nameof(Detail), new {id = personId});
        }


    }
}