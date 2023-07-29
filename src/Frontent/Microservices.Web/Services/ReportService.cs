using MassTransit;
using Microservices.Shared.Messages;
using Microservices.Shared.Models;

namespace Microservices.Web.Services
{
    public class ReportService : IReportService
    {
        private readonly HttpClient _httpClient;
        private readonly IPublishEndpoint _publishEndpoint;

        public ReportService(HttpClient httpClient, IPublishEndpoint publishEndpoint)
        {
            _httpClient = httpClient;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<List<ReportDto>> GetAll()
        {
            var reports = await _httpClient.GetFromJsonAsync<List<ReportDto>>("report");
            return reports;
        }

        public async Task<bool> GenerateReport(string location)
        {
            var createdReportId = await _httpClient.GetStringAsync($"report/createemptyreport?location={location}");

            await _publishEndpoint.Publish(new ReportRequestedEvent
            { 
                ReportId = createdReportId,
                Location = location
            });

            return true;
        }

    }
}
