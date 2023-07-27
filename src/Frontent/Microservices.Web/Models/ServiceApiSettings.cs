namespace Microservices.Web.Models
{
    public class ServiceApiSettings
    {
        public string GetawayBaseUri { get; set; }
        public ServiceApi PhoneBook { get; set; }
        public ServiceApi Reporter { get; set; }
    }

    public class ServiceApi
    {
        public string Path { get; set; }
    }
}