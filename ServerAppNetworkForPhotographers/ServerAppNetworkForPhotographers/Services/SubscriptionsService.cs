using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Data;
using ServerAppNetworkForPhotographers.Dtos.Subscriptions;
using ServerAppNetworkForPhotographers.Interfaces.Services;
using ServerAppNetworkForPhotographers.Models;

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
                throw new KeyNotFoundException($"Photographer with this id was not found");
            }
            if (!await CheckExistencePhotographer(subscriptionDto.SubscriberId))
            {
                throw new KeyNotFoundException("Photographer (subscriber) with this id was not found");
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
                throw new KeyNotFoundException("This subscription does not exists");

            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetCountSubscribers(int photographerId)
        {
            if (!await CheckExistencePhotographer(photographerId))
            {
                throw new KeyNotFoundException("Photographer with this id was not found");
            }

            return await _context.Subscriptions.CountAsync(item => item.PhotographerId == photographerId);
        }

        public async Task<int> GetCountSubscriptions(int photographerId)
        {
            if (!await CheckExistencePhotographer(photographerId))
            {
                throw new KeyNotFoundException("Photographer with this id was not found");
            }

            return await _context.Subscriptions.CountAsync(item => item.SubscriberId == photographerId);
        }

        public async Task<List<Photographer>> GetSubscribers(int photographerId)
        {
            if (!await CheckExistencePhotographer(photographerId))
            {
                throw new KeyNotFoundException("Photographer with this id was not found");
            }

            var subscribers = new List<Photographer>();

            await _context.Subscriptions
                .Include(item => item.Subscriber)
                .Where(item => item.PhotographerId == photographerId)
                .ForEachAsync(item => subscribers.Add(item.Subscriber));

            return subscribers;
        }

        public async Task<List<Photographer>> GetSubscriptions(int photographerId)
        {
            if (!await CheckExistencePhotographer(photographerId))
            {
                throw new KeyNotFoundException("Photographer with this id was not found");
            }

            var subscriptions = new List<Photographer>();

            await _context.Subscriptions
                .Include(item => item.Photographer)
                .Where(item => item.SubscriberId == photographerId)
                .ForEachAsync(item => subscriptions.Add(item.Photographer));

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
