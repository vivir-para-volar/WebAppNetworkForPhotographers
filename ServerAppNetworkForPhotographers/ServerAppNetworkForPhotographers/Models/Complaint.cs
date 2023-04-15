using ServerAppNetworkForPhotographers.Dtos.Complaints;
using System.Text.Json.Serialization;

namespace ServerAppNetworkForPhotographers.Models
{
    public class Complaint
    {
        public int Id { get; set; }
        public string Text { get; set; }

        [JsonIgnore]
        public List<Content> Contents { get; set; } = new List<Content>();

        public Complaint() { }

        public Complaint(CreateComplaintDto complaintDto)
        {
            Text = complaintDto.Text;
        }

        public void Update(UpdateComplaintDto complaintDto)
        {
            Text = complaintDto.Text;
        }
    }
}
