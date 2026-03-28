using System.Globalization;

namespace VolunteerHub.Models
{
    public enum OrganizationRole
    {
        OrganizationOwner,
        Admin,
        Recruiter,
        Volunteer
    }

    public class OrganizationMember : BaseModel
    {
        public string AccountId { get; set; }
        public virtual Account Account { get; set; }

        public string OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }

        public OrganizationRole OrganizationRole { get; set; }

        public long JoinedAt { get; set; } = DateTime.UtcNow.Ticks;
    }
}
