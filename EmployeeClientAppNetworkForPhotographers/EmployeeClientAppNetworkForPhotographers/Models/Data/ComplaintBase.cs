using System.Text.Json.Serialization;

namespace EmployeeClientAppNetworkForPhotographers.Models.Data
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

        private void InitLists()
        {
            Complaints = new List<Complaint>();
        }
    }
}
