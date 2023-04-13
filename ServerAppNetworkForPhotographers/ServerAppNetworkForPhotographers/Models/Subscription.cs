namespace ServerAppNetworkForPhotographers.Models
{
    public class Subscription
    {
        public int Id { get; set; }

        public int PhotographerId { get; set; }
        public Photographer Photographer { get; set; }

        public int SubscriberId { get; set; }
        public Photographer Subscriber { get; set; }
    }
}
