using System.ComponentModel.DataAnnotations;

namespace UserClientAppNetworkForPhotographers.Models.Identity.Dtos
{
    public class RegisterDto
    {
        [Required]
        [StringLength(32, MinimumLength = 4)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
