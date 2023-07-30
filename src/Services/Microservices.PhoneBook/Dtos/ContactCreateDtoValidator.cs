using FluentValidation;

namespace Microservices.PhoneBook.Dtos
{
    public class ContactCreateDtoValidator : AbstractValidator<ContactCreateDto>
    {
        public ContactCreateDtoValidator()
        {
            RuleFor(t => t.Id)
                .NotEmpty().WithMessage(ErrorMessages.Contact.EmptyId)
                .GreaterThan(0).WithMessage(ErrorMessages.Contact.NegativeId);
            RuleFor(t => t.PersonId).NotEmpty();
            RuleFor(t => t.ContactTypeId).NotEmpty();
            RuleFor(t => t.ContactContent).NotEmpty();
        }
    }
}
