using System.ComponentModel.DataAnnotations;

namespace Microservices.PhoneBook.Data
{
    public class Person
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Company { get; set; }

        public ICollection<ContactInfo> ContactInfos { get; set; } = new List<ContactInfo>();

    }
}
