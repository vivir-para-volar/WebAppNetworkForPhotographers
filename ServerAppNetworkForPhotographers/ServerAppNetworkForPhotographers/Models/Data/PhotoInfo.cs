using ServerAppNetworkForPhotographers.Models.Data.Dtos.PhotosInfo;
using System.Text.Json.Serialization;

namespace ServerAppNetworkForPhotographers.Models.Data
{
    public class PhotoInfo
    {
        public int Id { get; set; }

        public int PhotoId { get; set; }

        [JsonIgnore]
        public Photo Photo { get; set; }

        public PhotoInfo() { }

        public PhotoInfo(CreatePhotoInfoDto photoInfoDto)
        {
            PhotoId = photoInfoDto.PhotoId;
        }
    }
}
