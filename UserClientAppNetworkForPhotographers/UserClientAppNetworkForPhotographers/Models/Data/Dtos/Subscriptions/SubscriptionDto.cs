namespace UserClientAppNetworkForPhotographers.Models.Data.Dtos.Subscriptions
{
    public class SubscriptionDto
    {
        public int PhotographerId { get; set; }
        public int SubscriberId { get; set; }

        public SubscriptionDto() { }

        public SubscriptionDto(int photographerId, int subscriberId)
        {
            PhotographerId = photographerId;
            SubscriberId = subscriberId;
        }
    }
}
