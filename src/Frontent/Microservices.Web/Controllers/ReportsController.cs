using Microservices.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Web.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IPersonService _personService;
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task<IActionResult> Index()
        {
            var reports = await _reportService.GetAll();
            return View(reports);
        }

    }
}