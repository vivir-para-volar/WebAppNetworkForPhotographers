using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Models.Data.Dtos.Likes
{
    public class LikeDto
    {
        [Required]
        public int PhotographerId { get; set; }

        [Required]
        public int ContentId { get; set; }
    }
}
