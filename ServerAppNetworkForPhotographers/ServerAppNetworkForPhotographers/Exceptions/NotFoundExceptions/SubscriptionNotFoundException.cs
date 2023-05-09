namespace ServerAppNetworkForPhotographers.Exceptions.NotFoundExceptions
{
    public class SubscriptionNotFoundException : Exception
    {
        public override string Message { get; }

        public SubscriptionNotFoundException(int photographerId, int subscriberId) : base("")
        {
            Message = $"Subscription with photographerId = {photographerId} and subscriberId = {subscriberId} not found";
        }
    }
}
