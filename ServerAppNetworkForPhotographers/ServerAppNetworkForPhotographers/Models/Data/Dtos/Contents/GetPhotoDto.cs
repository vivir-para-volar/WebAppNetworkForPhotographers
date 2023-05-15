namespace ServerAppNetworkForPhotographers.Models.Data.Dtos.Contents
{
    public class GetPhotoDto
    {
        public int Id { get; set; }
        public string PhotoName { get; set; }
        public string PhotoContent { get; set; }

        public int ContentId { get; set; }

        public GetPhotoDto(int id, string photoName, string photoContent, int contentId)
        {
            Id = id;
            PhotoName = photoName;
            PhotoContent = photoContent;
            ContentId = contentId;
        }
    }
}
