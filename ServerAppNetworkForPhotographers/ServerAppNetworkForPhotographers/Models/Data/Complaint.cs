using ServerAppNetworkForPhotographers.Models.Dtos.Complaints;
using ServerAppNetworkForPhotographers.Models.Lists;
using System.Text.Json.Serialization;

namespace ServerAppNetworkForPhotographers.Models.Data
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

        public Complaint(CreateComplaintDto complaintDto)
        {
            Text = complaintDto.Text;
            ComplaintBaseId = complaintDto.ComplaintBaseId;
            ContentId = complaintDto.ContentId;

            Status = StatusComplaint.Open;
        }

        public void UpdateStatus()
        {
            Status = StatusComplaint.Processed;
        }
    }
}
