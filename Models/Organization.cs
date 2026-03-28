using System.ComponentModel.DataAnnotations;

namespace VolunteerHub.Models
{
    public class Organization
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Email { get; set; }
    }
}
