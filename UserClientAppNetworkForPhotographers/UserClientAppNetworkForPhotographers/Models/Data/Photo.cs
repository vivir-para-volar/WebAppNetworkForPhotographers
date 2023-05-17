using System.Text.Json.Serialization;

namespace UserClientAppNetworkForPhotographers.Models.Data
{
    public class Photo
    {
        public int Id { get; set; }
        public string PhotoContent { get; set; }

        public int ContentId { get; set; }

        [JsonIgnore]
        public Content Content { get; set; }

        [JsonIgnore]
        public PhotoInfo PhotoInfo { get; set; }

        public Photo() { }

        public Photo(string photoContent, int contentId)
        {
            this.PhotoContent = photoContent;
            this.ContentId = contentId;
        }
    }
}
