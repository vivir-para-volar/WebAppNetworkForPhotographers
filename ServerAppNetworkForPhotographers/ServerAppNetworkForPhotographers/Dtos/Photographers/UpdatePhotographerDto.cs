using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Dtos.Photographers
{
    public class UpdatePhotographerDto
    {
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 4)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
    }
}
