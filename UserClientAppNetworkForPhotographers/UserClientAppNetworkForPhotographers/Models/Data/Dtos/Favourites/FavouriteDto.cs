namespace UserClientAppNetworkForPhotographers.Models.Data.Dtos.Favourites
{
    public class FavouriteDto
    {
        public int PhotographerId { get; set; }
        public int ContentId { get; set; }

        public FavouriteDto() { }

        public FavouriteDto(int photographerId, int contentId)
        {
            PhotographerId = photographerId;
            ContentId = contentId;
        }
    }
}
