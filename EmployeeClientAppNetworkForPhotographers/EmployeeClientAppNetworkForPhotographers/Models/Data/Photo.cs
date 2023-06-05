using System.Text.Json.Serialization;

namespace EmployeeClientAppNetworkForPhotographers.Models.Data
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
    }
}
