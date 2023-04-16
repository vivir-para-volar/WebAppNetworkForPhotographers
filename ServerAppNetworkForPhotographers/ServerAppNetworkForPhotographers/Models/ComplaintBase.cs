using ServerAppNetworkForPhotographers.Dtos.Complaints;
using System.Text.Json.Serialization;

namespace ServerAppNetworkForPhotographers.Models
{
    public class ComplaintBase
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public List<Complaint> Complaints { get; set; } = new List<Complaint>();

        public ComplaintBase() { }

        public ComplaintBase(CreateComplaintBaseDto complaintDto)
        {
            Name = complaintDto.Name;
        }

        public void Update(UpdateComplaintBaseDto complaintDto)
        {
            Name = complaintDto.Name;
        }
    }
}
