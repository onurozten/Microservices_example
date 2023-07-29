using Microservices.Shared.Models;

namespace Microservices.Reporter.Services
{
    public interface IReportService
    {
        Task<List<ReportDto>> GetAllReports();
        Task<string> CreateEmptyReport(string location);
    }
}
