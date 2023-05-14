using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Photographers;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Subscriptions;

namespace ServerAppNetworkForPhotographers.Interfaces.Controllers
{
    public interface ISubscriptionsController
    {
        Task<ActionResult<bool>> CheckSubscription(SubscriptionDto subscriptionDto);
        Task<ActionResult<Subscription>> CreateSubscription(SubscriptionDto subscriptionDto);
        Task<ActionResult> DeleteSubscription(SubscriptionDto subscriptionDto);

        Task<ActionResult<int>> GetCountSubscribers(int photographerId);
        Task<ActionResult<int>> GetCountSubscriptions(int photographerId);
        Task<ActionResult<List<GetPhotographerForListDto>>> GetSubscribers(int photographerId);
        Task<ActionResult<List<GetPhotographerForListDto>>> GetSubscriptions(int photographerId);
    }
}
