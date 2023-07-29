namespace Microservices.Shared.Messages
{
    public class ReportFinishedEvent
    {
        public string ReportId { get; set; }
        public string Location { get; set; }
        public DateTime StaredAt { get; set; }
        public DateTime FinishedAd { get; set; }
        public int LocationCount { get; set; }
        public int PhoneCount { get; set; }
    }
}
