using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Models.Identity.Dtos
{
    public class LoginDto
    {
        [Required]
        [StringLength(32, MinimumLength = 4)]
        public string Login { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
