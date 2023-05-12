using ServerAppNetworkForPhotographers.Models.Data.Dtos.Subscriptions;
using System.Text.Json.Serialization;

namespace ServerAppNetworkForPhotographers.Models.Data
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

        public Subscription() { }

        public Subscription(SubscriptionDto subscriptionDto)
        {
            PhotographerId = subscriptionDto.PhotographerId;
            SubscriberId = subscriptionDto.SubscriberId;
        }
    }
}
