using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Models.Data.Dtos.Likes
{
    public class LikeDto
    {
        [Range(1, int.MaxValue)]
        public int PhotographerId { get; set; }

        [Range(1, int.MaxValue)]
        public int ContentId { get; set; }
    }
}
