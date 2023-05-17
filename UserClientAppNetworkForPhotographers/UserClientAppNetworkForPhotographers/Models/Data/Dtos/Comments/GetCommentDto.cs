using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Photographers;

namespace UserClientAppNetworkForPhotographers.Models.Data.Dtos.Comments
{
    public class GetCommentDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Text { get; set; }

        public GetPhotographerForListDto Photographer { get; set; }
        public int ContentId { get; set; }
    }
}
