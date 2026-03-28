using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace VolunteerHub.Models
{
    public enum Role
    {
        Admin,
        Volunteer,
        Superadmin
    }

    public class Account
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
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(200)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(200)]
        public string LastName { get; set; }

        [Required]
        public Role Role { get; set; }

        public ICollection<string>? OrganizationIds { get; set; }
        public virtual ICollection<Organization>? Organizations { get; set; }
    }
}
