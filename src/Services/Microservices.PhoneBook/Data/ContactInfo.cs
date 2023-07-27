using Microservices.PhoneBook.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microservices.PhoneBook.Data
{
    public class ContactInfo
    {
        public int Id { get; set; }

        public Person Person { get; set; }

        public string PersonId { get; set; }

        public int ContactTypeId { get; set; }

        [NotMapped]
        public ContactType ContactType
        {
            get
            {
                return (ContactType)ContactTypeId;
            }
            set
            {
                ContactTypeId = (int)value;
            }
        }
    }
}
