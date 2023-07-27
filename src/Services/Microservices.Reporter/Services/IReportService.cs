using Microservices.Shared.Models;

namespace Microservices.Reporter.Services
{
    public interface IReportService
    {
        Task<List<ReportDto>> GetAllReports();
    }
}
