using Microservices.Shared.Models;

namespace Microservices.Web.Services
{
    public interface IReportService
    {
        Task<List<ReportDto>> GetAll();
        Task<bool> GenerateReport(string location);
    }
}
