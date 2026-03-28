using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VolunteerHub.Models;

namespace VolunteerHub.Database
{
    public class DBM : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DBM(DbContextOptions<DBM> options, IHttpContextAccessor httpContextAccessor) : base(options) 
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationMember> OrganizationMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DBMTools.ConfigureModel(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseModel>();

            var userId = _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;

            foreach (var entry in entries)
            {
                long now = DateTime.UtcNow.Ticks;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = now;
                    entry.Entity.CreatedBy = userId;
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    entry.Entity.ModifiedAt = now;
                }

                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.Deleted = true;
                }

                entry.Entity.LastModifiedBy = userId;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
