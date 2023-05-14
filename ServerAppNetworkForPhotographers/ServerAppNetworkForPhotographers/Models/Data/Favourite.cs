using ServerAppNetworkForPhotographers.Models.Data.Dtos.Favourites;
using System.Text.Json.Serialization;

namespace ServerAppNetworkForPhotographers.Models.Data
{
    public class Favourite
    {
        public int Id { get; set; }

        public int PhotographerId { get; set; }
        public int ContentId { get; set; }

        [JsonIgnore]
        public Photographer Photographer { get; set; }

        [JsonIgnore]
        public Content Content { get; set; }

        public Favourite() { }

        public Favourite(FavouriteDto favouriteDto)
        {
            PhotographerId = favouriteDto.PhotographerId;
            ContentId = favouriteDto.ContentId;
        }
    }
}
