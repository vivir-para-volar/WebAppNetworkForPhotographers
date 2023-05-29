namespace UserClientAppNetworkForPhotographers.Models.Data.Dtos.Comments
{
    public class CreateCommentDto
    {
        public string Text { get; set; }
        public int PhotographerId { get; set; }
        public int ContentId { get; set; }

        public CreateCommentDto() { }

        public CreateCommentDto(string text, int photographerId, int contentId)
        {
            Text = text;
            PhotographerId = photographerId;
            ContentId = contentId;
        }
    }
}
