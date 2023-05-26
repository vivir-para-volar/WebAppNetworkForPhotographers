namespace UserClientAppNetworkForPhotographers.Models.Data.Dtos.Likes
{
    public class LikeDto
    {
        public int PhotographerId { get; set; }
        public int ContentId { get; set; }

        public LikeDto() { }

        public LikeDto(int photographerId, int contentId)
        {
            PhotographerId = photographerId;
            ContentId = contentId;
        }
    }
}
