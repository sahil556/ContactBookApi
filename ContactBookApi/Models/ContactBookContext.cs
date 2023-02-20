using Microsoft.EntityFrameworkCore;
using ContactBookApi.Models;

namespace ContactBookApi.Models
{
    public class ContactBookContext : DbContext
    {
        public ContactBookContext(DbContextOptions<ContactBookContext> options) : base(options)
        {

        }
        public DbSet<ContactItem> Contacts { get; set; } = null!;
        public DbSet<Address> Addresses { get; set; } = null!;
        public DbSet<ContactBookApi.Models.MobileNumber> MobileNumber { get; set; }

        public DbSet<User> Users { get; set; } = null!;

    }
}
