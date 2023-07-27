using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Microservices.PhoneBook.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }

        
    }
}
