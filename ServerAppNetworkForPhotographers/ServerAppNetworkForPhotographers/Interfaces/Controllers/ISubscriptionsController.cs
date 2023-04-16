using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Dtos.Subscriptions;
using ServerAppNetworkForPhotographers.Models;

namespace ServerAppNetworkForPhotographers.Interfaces.Controllers
{
    public interface ISubscriptionsController
    {
        Task<ActionResult<bool>> CheckSubscription(SubscriptionDto subscriptionDto);
        Task<ActionResult> CreateSubscription(SubscriptionDto subscriptionDto);
        Task<ActionResult> DeleteSubscription(SubscriptionDto subscriptionDto);

        Task<ActionResult<int>> GetCountSubscribers(int photographerId);
        Task<ActionResult<int>> GetCountSubscriptions(int photographerId);
        Task<ActionResult<List<Photographer>>> GetSubscribers(int photographerId);
        Task<ActionResult<List<Photographer>>> GetSubscriptions(int photographerId);
    }
}
