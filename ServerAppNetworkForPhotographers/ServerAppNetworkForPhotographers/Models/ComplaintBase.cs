using ServerAppNetworkForPhotographers.Dtos.Complaints;
using System.Text.Json.Serialization;

namespace ServerAppNetworkForPhotographers.Models
{
    public class ComplaintBase
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public List<Complaint> Complaints { get; set; }

        public ComplaintBase()
        {
            InitLists();
        }

        public ComplaintBase(CreateComplaintBaseDto complaintBaseDto)
        {
            Name = complaintBaseDto.Name;

            InitLists();
        }

        public void Update(UpdateComplaintBaseDto complaintBaseDto)
        {
            Name = complaintBaseDto.Name;
        }

        private void InitLists()
        {
            Complaints = new List<Complaint>();
        }
    }
}
