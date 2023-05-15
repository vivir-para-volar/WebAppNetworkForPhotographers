using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Models.Data.Dtos.Photographers
{
    public class CreatePhotographerDto
    {
        [Required]
        [StringLength(32, MinimumLength = 4)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserId { get; set; }

        public CreatePhotographerDto() { }

        public CreatePhotographerDto(string username, string email, string userId)
        {
            Username = username;
            Email = email;
            UserId = userId;
        }
    }
}
