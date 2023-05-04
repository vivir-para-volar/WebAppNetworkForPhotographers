using System.Text.Json.Serialization;

namespace ServerAppNetworkForPhotographers.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Text { get; set; }

        public int PhotographerId { get; set; }
        public int ContentId { get; set; }

        [JsonIgnore]
        public Photographer Photographer { get; set; }

        [JsonIgnore]
        public Content Content { get; set; }
    }
}
