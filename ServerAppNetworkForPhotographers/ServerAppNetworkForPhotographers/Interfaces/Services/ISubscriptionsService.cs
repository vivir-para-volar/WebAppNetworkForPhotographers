using ServerAppNetworkForPhotographers.Models.Dtos.Photographers;
using ServerAppNetworkForPhotographers.Models.Dtos.Subscriptions;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface ISubscriptionsService
    {
        Task<bool> CheckSubscription(SubscriptionDto subscriptionDto);
        Task CreateSubscription(SubscriptionDto subscriptionDto);
        Task DeleteSubscription(SubscriptionDto subscriptionDto);

        Task<int> GetCountSubscribers(int photographerId);
        Task<int> GetCountSubscriptions(int photographerId);
        Task<List<GetPhotographerForListDto>> GetSubscribers(int photographerId);
        Task<List<GetPhotographerForListDto>> GetSubscriptions(int photographerId);
    }
}
