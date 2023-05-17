using System.Text.Json.Serialization;

namespace UserClientAppNetworkForPhotographers.Models.Data
{
    public class Complaint
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public string Status { get; set; }

        public int ComplaintBaseId { get; set; }
        public int ContentId { get; set; }

        public ComplaintBase ComplaintBase { get; set; }

        [JsonIgnore]
        public Content Content { get; set; }

        public Complaint() { }
    }
}
