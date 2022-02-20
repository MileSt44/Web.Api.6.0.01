using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace PhoneBookApi.Models
{
    public class PhonebookContext : DbContext
    {
        public PhonebookContext(DbContextOptions<PhonebookContext> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
    }
}
