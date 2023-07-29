namespace Microservices.Shared.Messages
{
    public class ReportRequestedEvent
    {
        public string ReportId { get; set; }
        public string Location { get; set; }
    }
}
