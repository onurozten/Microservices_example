using Microservices.Reporter.Services;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Reporter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var reports = await _reportService.GetAllReports();
            return Ok(reports);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CreateEmptyReport(string location)
        {
            var reportId = await _reportService.CreateEmptyReport(location);
            return Ok(reportId);
        }
    }
}