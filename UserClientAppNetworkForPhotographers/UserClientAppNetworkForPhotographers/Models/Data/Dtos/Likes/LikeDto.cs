using System.ComponentModel.DataAnnotations;

namespace UserClientAppNetworkForPhotographers.Models.Data.Dtos.Likes
{
    public class LikeDto
    {
        [Range(1, int.MaxValue)]
        public int PhotographerId { get; set; }

        [Range(1, int.MaxValue)]
        public int ContentId { get; set; }
    }
}
