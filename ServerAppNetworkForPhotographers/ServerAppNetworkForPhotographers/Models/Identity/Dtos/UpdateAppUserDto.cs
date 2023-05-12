using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Models.Identity.Dtos
{
    public class UpdateAppUserDto
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 4)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
