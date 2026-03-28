using Microsoft.EntityFrameworkCore;
using VolunteerHub.Models;

namespace VolunteerHub.Database
{
    public class DBM : DbContext
    {
        public DBM(DbContextOptions<DBM> options) : base(options) { }
        public DbSet<Account> Accounts { get; set; }
    }
}
