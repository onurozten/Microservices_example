using AutoMapper;
using Microservices.PhoneBook.Data;
using Microservices.PhoneBook.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Microservices.PhoneBook.Services
{
    public class PersonService : IPersonService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public PersonService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonDto>> GetAll()
        {
            var people = await _appDbContext.People.ToListAsync();

            var dtos = _mapper.Map<IEnumerable<PersonDto>>(people);

            return dtos;
        }
    }
}
