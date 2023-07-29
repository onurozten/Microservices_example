using AutoMapper;
using Microservices.PhoneBook.Data;
using Microservices.PhoneBook.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Microservices.PhoneBook.Services
{
    public class PersonService : IPersonService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public PersonService(AppDbContext appDbContext, IMapper mapper)
        {
            _dbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonDto>> GetAll()
        {
            var people = await _dbContext.People.ToListAsync();

            var dtos = _mapper.Map<IEnumerable<PersonDto>>(people);

            return dtos;
        }

        public async Task<PersonDto> GetById(string id)
        {
            var people = await _dbContext.People
                .Include(x => x.ContactInfos)
                .FirstOrDefaultAsync(x => x.Id == id);

            var dto = _mapper.Map<PersonDto>(people);

            return dto;
        }

        public async Task DeleteById(string id)
        {
            var person = await _dbContext.People
                .FirstOrDefaultAsync(x => x.Id == id);

            if (person != null)
            {
                _dbContext.People.Remove(person);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task Create(PersonCreateDto createDto)
        {
            var person = _mapper.Map<Person>(createDto);
            person.Id = Guid.NewGuid().ToString();  // default value olabilir mi?

            _dbContext.People.Add(person);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateDetail(ContactCreateDto createDto)
        {
            var contactInfo = _mapper.Map<ContactInfo>(createDto);

            _dbContext.ContactInfos.Add(contactInfo);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteDetailById(int id)
        {
            var contactInfo = await _dbContext.ContactInfos
                .FirstOrDefaultAsync(x => x.Id == id);

            if (contactInfo != null)
            {
                _dbContext.ContactInfos.Remove(contactInfo);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
