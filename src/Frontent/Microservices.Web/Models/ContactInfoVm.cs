using Microservices.Shared.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microservices.Web.Models
{
    public class ContactInfoVm
    {
        public int Id { get; set; }

        public PersonVm Person { get; set; }

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

        public string ContactContent { get; set; }
    }
}