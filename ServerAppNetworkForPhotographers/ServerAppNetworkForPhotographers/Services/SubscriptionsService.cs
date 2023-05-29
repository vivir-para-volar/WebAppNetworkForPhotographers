using Microsoft.EntityFrameworkCore;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Photographers;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Subscriptions;

namespace ServerAppNetworkForPhotographers.Services
{
    public class SubscriptionsService
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

        public async Task<Subscription> CreateSubscription(SubscriptionDto subscriptionDto)
        {
            if (!await CheckExistencePhotographer(subscriptionDto.PhotographerId))
            {
                throw new NotFoundException(nameof(Photographer), subscriptionDto.PhotographerId);
            }
            if (!await CheckExistencePhotographer(subscriptionDto.SubscriberId))
            {
                throw new NotFoundException(nameof(Photographer), subscriptionDto.SubscriberId);
            }

            if (subscriptionDto.PhotographerId == subscriptionDto.SubscriberId)
            {
                throw new InvalidOperationException(
                    $"{nameof(subscriptionDto.PhotographerId)} and {nameof(subscriptionDto.SubscriberId)} must be different"
                );
            }

            if (await CheckSubscription(subscriptionDto))
            {
                throw new UniqueModelException(nameof(Subscription));
            }

            var subscription = new Subscription(subscriptionDto);

            await _context.Subscriptions.AddAsync(subscription);
            await _context.SaveChangesAsync();

            return subscription;
        }

        public async Task DeleteSubscription(SubscriptionDto subscriptionDto)
        {
            var subscription = (await GetSubscription(subscriptionDto)) ??
                throw new NotFoundException(subscriptionDto);

            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetCountSubscribers(int photographerId)
        {
            if (!await CheckExistencePhotographer(photographerId))
            {
                throw new NotFoundException(nameof(Photographer), photographerId);
            }

            return await _context.Subscriptions.CountAsync(item => item.PhotographerId == photographerId);
        }

        public async Task<int> GetCountSubscriptions(int photographerId)
        {
            if (!await CheckExistencePhotographer(photographerId))
            {
                throw new NotFoundException(nameof(Photographer), photographerId);
            }

            return await _context.Subscriptions.CountAsync(item => item.SubscriberId == photographerId);
        }

        public async Task<List<GetPhotographerForListDto>> GetSubscribers(int photographerId)
        {
            if (!await CheckExistencePhotographer(photographerId))
            {
                throw new NotFoundException(nameof(Photographer), photographerId);
            }

            var subscribers = new List<Photographer>();
            await _context.Subscriptions
                .Include(item => item.Subscriber)
                .Where(item => item.PhotographerId == photographerId)
                .ForEachAsync((item) => subscribers.Add(item.Subscriber));

            return Photographer.ToListGetPhotographerForListDto(subscribers);
        }

        public async Task<List<GetPhotographerForListDto>> GetSubscriptions(int photographerId)
        {
            if (!await CheckExistencePhotographer(photographerId))
            {
                throw new NotFoundException(nameof(Photographer), photographerId);
            }

            var subscriptions = new List<Photographer>();
            await _context.Subscriptions
                .Include(item => item.Photographer)
                .Where(item => item.SubscriberId == photographerId)
                .ForEachAsync((item) => subscriptions.Add(item.Photographer));

            return Photographer.ToListGetPhotographerForListDto(subscriptions);
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
