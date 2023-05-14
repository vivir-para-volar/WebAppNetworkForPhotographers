using ServerAppNetworkForPhotographers.Models.Data.Dtos.Photographers;

namespace ServerAppNetworkForPhotographers.Models.Data.Dtos.Comments
{
    public class GetCommentDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Text { get; set; }

        public GetPhotographerForListDto Photographer { get; set; }
        public int ContentId { get; set; }

        public GetCommentDto(int id, DateTime createdAt, string text, GetPhotographerForListDto photographer, int contentId)
        {
            Id = id;
            CreatedAt = createdAt;
            Text = text;
            Photographer = photographer;
            ContentId = contentId;
        }
    }
}
