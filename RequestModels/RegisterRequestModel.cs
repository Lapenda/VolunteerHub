using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace VolunteerHub.RequestModels
{
    public class RegisterRequestModel
    {
        [Required]
        [MaxLength(200)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [MaxLength(200)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(200)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; }
    }
}
