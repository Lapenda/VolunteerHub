using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteerHub.Models
{
    public enum GlobalRole
    {
        User,
        SystemAdmin
    }

    public class Account : BaseModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(200)]
        public string Username { get; set; }

        [Required]
        [MaxLength(200)]
        public string Password { get; set; }

        [Required]
        [MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(200)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(200)]
        public string LastName { get; set; }

        [Required]
        public GlobalRole GlobalRole { get; set; }

        public virtual ICollection<OrganizationMember> Memberships { get; set; } = new List<OrganizationMember>();
    }
}
