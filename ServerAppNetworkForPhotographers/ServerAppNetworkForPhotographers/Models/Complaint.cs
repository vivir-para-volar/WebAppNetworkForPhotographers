using System.Text.Json.Serialization;

namespace ServerAppNetworkForPhotographers.Models
{
    public class Complaint
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public string Status { get; set; }

        public int ContentId { get; set; }
        public int? ComplaintBaseId { get; set; }

        [JsonIgnore]
        public Content Content { get; set; }

        [JsonIgnore]
        public ComplaintBase ComplaintBase { get; set; }
    }
}
