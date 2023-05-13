using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Subscriptions;

namespace ServerAppNetworkForPhotographers.Exceptions
{
    public class NotFoundException : Exception
    {
        public override string Message { get; }

        public NotFoundException(string model, int id) : base("")
        {
            Message = $"{model} with id = {id} not found";
        }

        public NotFoundException(string model, string? id) : base("")
        {
            if (id == null)
            {
                Message = $"{model} not found";
            }

            Message = $"{model} with id = {id} not found";
        }

        public NotFoundException(SubscriptionDto subscriptionDto) : base("")
        {
            Message = $"{nameof(Subscription)} with photographerId = {subscriptionDto.PhotographerId} and" +
                $"subscriberId = {subscriptionDto.SubscriberId} not found";
        }
    }
}
