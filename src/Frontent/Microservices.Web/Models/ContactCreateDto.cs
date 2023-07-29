namespace Microservices.Web.Models
{
    public class ContactCreateModel
    {
        public int Id { get; set; }

        public string PersonId { get; set; }

        public int ContactTypeId { get; set; }

        public string ContactContent { get; set; }
    }
}