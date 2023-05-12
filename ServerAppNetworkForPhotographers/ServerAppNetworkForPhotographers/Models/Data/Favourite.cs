using System.Text.Json.Serialization;

namespace ServerAppNetworkForPhotographers.Models.Data
{
    public class Favourite
    {
        public int Id { get; set; }

        public int PhotographerId { get; set; }
        public int ContentId { get; set; }

        [JsonIgnore]
        public Photographer Photographer { get; set; }

        [JsonIgnore]
        public Content Content { get; set; }
    }
}
