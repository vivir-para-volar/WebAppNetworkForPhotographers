using System.Text.Json.Serialization;

namespace EmployeeClientAppNetworkForPhotographers.Models.Data
{
    public class Subscription
    {
        public int Id { get; set; }

        public int PhotographerId { get; set; }
        public int SubscriberId { get; set; }

        [JsonIgnore]
        public Photographer Photographer { get; set; }

        [JsonIgnore]
        public Photographer Subscriber { get; set; }
    }
}
