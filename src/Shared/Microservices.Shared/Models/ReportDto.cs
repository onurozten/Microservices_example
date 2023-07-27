using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Microservices.Shared.Models
{
    public class ReportDto
    {
        public string Id { get; set; }

        public string Location { get; set; }

        public DateTime StaredAt { get; set; }

        public DateTime? FinishedAd { get; set; }
        public int LocationCount { get; set; }
        public int PhoneCount { get; set; }
    }
}
