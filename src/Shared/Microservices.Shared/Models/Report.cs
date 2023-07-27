using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Microservices.Shared.Models
{
    public class Report
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Location { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime StaredAt { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime? FinishedAd { get; set; }
        public int LocationCount { get; set; }
        public int PhoneCount { get; set; }
    }
}
