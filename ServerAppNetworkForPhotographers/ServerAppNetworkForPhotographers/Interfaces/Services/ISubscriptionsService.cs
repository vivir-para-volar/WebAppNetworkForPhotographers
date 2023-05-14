using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Photographers;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Subscriptions;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface ISubscriptionsService
    {
        Task<bool> CheckSubscription(SubscriptionDto subscriptionDto);
        Task<Subscription> CreateSubscription(SubscriptionDto subscriptionDto);
        Task DeleteSubscription(SubscriptionDto subscriptionDto);

        Task<int> GetCountSubscribers(int photographerId);
        Task<int> GetCountSubscriptions(int photographerId);
        Task<List<GetPhotographerForListDto>> GetSubscribers(int photographerId);
        Task<List<GetPhotographerForListDto>> GetSubscriptions(int photographerId);
    }
}
