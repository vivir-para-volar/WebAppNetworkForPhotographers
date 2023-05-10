﻿using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Models.Dtos.Photographers;
using ServerAppNetworkForPhotographers.Models.Dtos.Subscriptions;

namespace ServerAppNetworkForPhotographers.Interfaces.Controllers
{
    public interface ISubscriptionsController
    {
        Task<ActionResult<bool>> CheckSubscription(SubscriptionDto subscriptionDto);
        Task<ActionResult> CreateSubscription(SubscriptionDto subscriptionDto);
        Task<ActionResult> DeleteSubscription(SubscriptionDto subscriptionDto);

        Task<ActionResult<int>> GetCountSubscribers(int photographerId);
        Task<ActionResult<int>> GetCountSubscriptions(int photographerId);
        Task<ActionResult<List<GetPhotographerForListDto>>> GetSubscribers(int photographerId);
        Task<ActionResult<List<GetPhotographerForListDto>>> GetSubscriptions(int photographerId);
    }
}