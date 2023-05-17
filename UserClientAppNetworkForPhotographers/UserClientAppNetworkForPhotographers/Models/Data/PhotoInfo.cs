using System.Text.Json.Serialization;

namespace UserClientAppNetworkForPhotographers.Models.Data
{
    public class PhotoInfo
    {
        public int Id { get; set; }

        public int PhotoId { get; set; }

        [JsonIgnore]
        public Photo Photo { get; set; }

        public PhotoInfo() { }
    }
}
