using AutoMapper;
using Microservices.Reporter.Model;
using Microservices.Shared.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Microservices.Reporter.Services
{
    public class ReportService : IReportService
    {

        private readonly IMongoCollection<Report> _reportCollection;
        private readonly IMapper _mapper;

        public ReportService(IOptions<DatabaseSettings> databaseSettings, IMapper mapper)
        {
            var client = new MongoClient(databaseSettings.Value.ConnectionString);
            var database = client.GetDatabase(databaseSettings.Value.DatabaseName);
            _reportCollection = database.GetCollection<Report>(databaseSettings.Value.CollectionName);
            _mapper = mapper;
        }

        public async Task<List<ReportDto>> GetAllReports()
        {
            var reports = await _reportCollection.Find(x=>true).ToListAsync();
            var dtos = _mapper.Map<List<ReportDto>>(reports);

            return dtos;
        }
    }
}
