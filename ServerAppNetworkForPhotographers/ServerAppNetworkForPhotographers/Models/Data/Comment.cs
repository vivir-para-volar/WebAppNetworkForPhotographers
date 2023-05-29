using ServerAppNetworkForPhotographers.Models.Data.Dtos.Comments;
using System.Text.Json.Serialization;

namespace ServerAppNetworkForPhotographers.Models.Data
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Text { get; set; }

        public int PhotographerId { get; set; }
        public int ContentId { get; set; }

        [JsonIgnore]
        public Photographer Photographer { get; set; }

        [JsonIgnore]
        public Content Content { get; set; }

        public Comment() { }

        public Comment(CreateCommentDto commentDto)
        {
            CreatedAt = DateTime.Now;

            Text = commentDto.Text;
            PhotographerId = commentDto.PhotographerId;
            ContentId = commentDto.ContentId;
        }

        public GetCommentDto ToGetCommentDto()
        {
            var photographer = Photographer.ToGetPhotographerForListDto();
            return new GetCommentDto(Id, CreatedAt, Text, photographer, ContentId);
        }

        public static List<GetCommentDto> ToListGetCommentDto(List<Comment> comments)
        {
            var getComments = new List<GetCommentDto>();
            foreach (var comment in comments)
            {
                getComments.Add(comment.ToGetCommentDto());
            }
            return getComments;
        }
    }
}
