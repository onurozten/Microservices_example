using Microservices.Shared.Enums;

namespace Microservices.PhoneBook.Dtos
{
    public class ContactInfoDto
    {
        public int Id { get; set; }

        public string PersonId { get; set; }

        public int ContactTypeId { get; set; }

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

        public string ContactContent { get; set; }

    }
}
