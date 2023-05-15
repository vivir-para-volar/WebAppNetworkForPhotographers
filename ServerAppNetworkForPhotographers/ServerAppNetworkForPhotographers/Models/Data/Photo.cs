using ServerAppNetworkForPhotographers.Files;
using System.Text.Json.Serialization;

namespace ServerAppNetworkForPhotographers.Models.Data
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

        public Photo (string photoContent, int contentId)
        {
            this.PhotoContent = photoContent;
            this.ContentId = contentId;
        }

        public async Task ConvertContentPhoto()
        {
            PhotoContent = await FileInteraction.GetBase64ContentPhoto(ContentId, PhotoContent);
        }

        public static async Task<Photo> Save(int contentId, IFormFile photo)
        {
            var photoContentName = await FileInteraction.SaveContentPhoto(contentId, photo);
            return new Photo(photoContentName, contentId);
        }

        public static void DeleteAllByContentId(int contentId)
        {
            FileInteraction.DeleteContentPhotos(contentId);
        }
    }
}
