using Microservices.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Web.Controllers
{
    public class ReportsController : Controller
    {
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

        [HttpPost]
        public async Task<IActionResult> Create(string location)
        {
            var reports = await _reportService.GenerateReport(location);
            return RedirectToAction(nameof(Index));
        }

    }
}