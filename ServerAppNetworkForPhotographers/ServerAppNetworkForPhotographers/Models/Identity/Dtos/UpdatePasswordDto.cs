using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Models.Identity.Dtos
{
    public class UpdatePasswordDto
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 6)]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 6)]
        public string NewPassword { get; set; }
    }
}
