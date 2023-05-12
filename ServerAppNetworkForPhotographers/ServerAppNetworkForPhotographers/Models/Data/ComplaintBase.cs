using ServerAppNetworkForPhotographers.Models.Dtos.ComplaintsBase;
using System.Text.Json.Serialization;

namespace ServerAppNetworkForPhotographers.Models.Data
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
            InitLists();

            Name = complaintBaseDto.Name;
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
