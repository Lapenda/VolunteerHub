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
        }
    }
}
