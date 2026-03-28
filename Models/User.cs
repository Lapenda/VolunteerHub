using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace VolunteerHub.Models
{
    public enum Role
    {
        Organization,
        Volunteer,
        Superadmin
    }

    public class User
    {
        [Key]
        public string Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Username { get; set; }

        [Required]
        [MaxLength(200)]
        public string Password { get; set; }

        [Required]
        [MaxLength(200)]
        public string Email { get; set; }

        [Required]
        [MaxLength(200)]
        public string FullName { get; set; }

        [Required]
        public Role Role { get; set; }

        public string? OrganizationId { get; set; }
        public virtual Organization? Organization { get; set; }
    }
}
