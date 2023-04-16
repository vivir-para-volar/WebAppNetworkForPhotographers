using ServerAppNetworkForPhotographers.Dtos.Subscriptions;
using ServerAppNetworkForPhotographers.Models;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface ISubscriptionsService
    {
        Task<bool> CheckSubscription(SubscriptionDto subscriptionDto);
        Task CreateSubscription(SubscriptionDto subscriptionDto);
        Task DeleteSubscription(SubscriptionDto subscriptionDto);

        Task<int> GetCountSubscribers(int photographerId);
        Task<int> GetCountSubscriptions(int photographerId);
        Task<List<Photographer>> GetSubscribers(int photographerId);
        Task<List<Photographer>> GetSubscriptions(int photographerId);
    }
}
