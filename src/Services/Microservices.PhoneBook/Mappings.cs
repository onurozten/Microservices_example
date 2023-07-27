using AutoMapper;
using Microservices.PhoneBook.Data;
using Microservices.PhoneBook.Dtos;

namespace Microservices.PhoneBook
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Person, PersonDto>().ReverseMap();
        }
    }
}
