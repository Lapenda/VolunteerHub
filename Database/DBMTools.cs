using Microsoft.EntityFrameworkCore;
using VolunteerHub.Models;

namespace VolunteerHub.Database
{
    public static class DBMTools
    {
        public static void ConfigureModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrganizationMember>()
                .HasKey(om => new { om.AccountId, om.OrganizationId });

            // Organization
            modelBuilder.Entity<Organization>()
                .HasMany(o => o.Members).WithOne(o => o.Organization).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Organization>()
                .HasOne(o => o.OrganizationOwner).WithOne().OnDelete(DeleteBehavior.NoAction);
        }
    }
}
