using System.Text.Json.Serialization;

namespace UserClientAppNetworkForPhotographers.Models.Data
{
    public class Like
    {
        public int Id { get; set; }

        public int PhotographerId { get; set; }
        public int ContentId { get; set; }

        [JsonIgnore]
        public Photographer Photographer { get; set; }

        [JsonIgnore]
        public Content Content { get; set; }

        public Like() { }
    }
}
