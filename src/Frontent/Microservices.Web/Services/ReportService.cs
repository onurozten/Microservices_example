using Microservices.Shared.Models;

namespace Microservices.Web.Services
{
    public class ReportService : IReportService
    {
        private readonly HttpClient _httpClient;

        public ReportService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ReportDto>> GetAll()
        {
            var reports = await _httpClient.GetFromJsonAsync<List<ReportDto>>("report");
            return reports;
        }
    }
}
