using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Exceptions.NotFoundExceptions;
using ServerAppNetworkForPhotographers.Interfaces.Services;
using ServerAppNetworkForPhotographers.Models;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Dtos.Photographers;
using ServerAppNetworkForPhotographers.Models.Dtos.Subscriptions;

namespace ServerAppNetworkForPhotographers.Services
{
    public class SubscriptionsService : ISubscriptionsService
    {
        private readonly DataContext _context;

        public SubscriptionsService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckSubscription(SubscriptionDto subscriptionDto)
        {
            int photographerId = subscriptionDto.PhotographerId;
            int subscriberId = subscriptionDto.SubscriberId;

            return await _context.Subscriptions
                .AnyAsync(item => item.PhotographerId == photographerId && item.SubscriberId == subscriberId);
        }

        public async Task CreateSubscription(SubscriptionDto subscriptionDto)
        {
            if (!await CheckExistencePhotographer(subscriptionDto.PhotographerId))
            {
                throw new PhotographerNotFoundException(subscriptionDto.PhotographerId);
            }
            if (!await CheckExistencePhotographer(subscriptionDto.SubscriberId))
            {
                throw new PhotographerNotFoundException(subscriptionDto.SubscriberId);
            }

            if (subscriptionDto.PhotographerId == subscriptionDto.SubscriberId)
            {
                throw new InvalidOperationException("Photographer and subscriber must be different");
            }

            if (await CheckSubscription(subscriptionDto))
            {
                throw new InvalidOperationException("This subscription already exists");
            }

            var subscription = new Subscription(subscriptionDto);

            await _context.Subscriptions.AddAsync(subscription);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSubscription(SubscriptionDto subscriptionDto)
        {
            var subscription = (await GetSubscription(subscriptionDto)) ??
                throw new SubscriptionNotFoundException(subscriptionDto.PhotographerId, subscriptionDto.SubscriberId);

            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetCountSubscribers(int photographerId)
        {
            if (!await CheckExistencePhotographer(photographerId))
            {
                throw new PhotographerNotFoundException(photographerId);
            }

            return await _context.Subscriptions.CountAsync(item => item.PhotographerId == photographerId);
        }

        public async Task<int> GetCountSubscriptions(int photographerId)
        {
            if (!await CheckExistencePhotographer(photographerId))
            {
                throw new PhotographerNotFoundException(photographerId);
            }

            return await _context.Subscriptions.CountAsync(item => item.SubscriberId == photographerId);
        }

        public async Task<List<GetPhotographerForListDto>> GetSubscribers(int photographerId)
        {
            if (!await CheckExistencePhotographer(photographerId))
            {
                throw new PhotographerNotFoundException(photographerId);
            }

            var subscribers = new List<GetPhotographerForListDto>();

            await _context.Subscriptions
                .Include(item => item.Subscriber)
                .Where(item => item.PhotographerId == photographerId)
                .ForEachAsync(async (item) => subscribers.Add(await item.Subscriber.ToGetPhotographerForListDto()));

            return subscribers;
        }

        public async Task<List<GetPhotographerForListDto>> GetSubscriptions(int photographerId)
        {
            if (!await CheckExistencePhotographer(photographerId))
            {
                throw new PhotographerNotFoundException(photographerId);
            }

            var subscriptions = new List<GetPhotographerForListDto>();

            await _context.Subscriptions
                .Include(item => item.Photographer)
                .Where(item => item.SubscriberId == photographerId)
                .ForEachAsync(async (item) => subscriptions.Add(await item.Photographer.ToGetPhotographerForListDto()));

            return subscriptions;
        }

        private async Task<Subscription?> GetSubscription(SubscriptionDto subscriptionDto)
        {
            int photographerId = subscriptionDto.PhotographerId;
            int subscriberId = subscriptionDto.SubscriberId;

            return await _context.Subscriptions
                .FirstOrDefaultAsync(item => item.PhotographerId == photographerId && item.SubscriberId == subscriberId);
        }

        private async Task<bool> CheckExistencePhotographer(int photographerId)
        {
            return await _context.Photographers.AnyAsync(item => item.Id == photographerId);
        }
    }
}
