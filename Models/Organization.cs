using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolunteerHub.Models
{
    public class Organization : BaseModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Email { get; set; }

        public string? OrganizationOwnerId { get; set; }
        public virtual Account? OrganizationOwner { get; set; }

        public virtual ICollection<OrganizationMember> Members { get; set; } = new List<OrganizationMember>();
    }
}
