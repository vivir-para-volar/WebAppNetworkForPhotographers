using System.ComponentModel.DataAnnotations;

namespace ServerAppNetworkForPhotographers.Models.Data.Dtos.Favourites
{
    public class FavouriteDto
    {
        [Required]
        public int PhotographerId { get; set; }

        [Required]
        public int ContentId { get; set; }
    }
}
