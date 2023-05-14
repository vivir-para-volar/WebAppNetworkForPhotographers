using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Favourites;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Likes;
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
            Message = $"{nameof(Subscription)} with " +
                $"{nameof(subscriptionDto.PhotographerId)} = {subscriptionDto.PhotographerId} and" +
                $"{nameof(subscriptionDto.SubscriberId)} = {subscriptionDto.SubscriberId} not found";
        }

        public NotFoundException(LikeDto likeDto) : base("")
        {
            Message = $"{nameof(Like)} with " +
                $"{nameof(likeDto.PhotographerId)} = {likeDto.PhotographerId} and " +
                $"{nameof(likeDto.ContentId)} = {likeDto.ContentId} not found";
        }

        public NotFoundException(FavouriteDto favouriteDto) : base("")
        {
            Message = $"{nameof(Favourite)} with " +
                $"{nameof(favouriteDto.PhotographerId)} = {favouriteDto.PhotographerId} and " +
                $"{nameof(favouriteDto.ContentId)} = {favouriteDto.ContentId} not found";
        }
    }
}
