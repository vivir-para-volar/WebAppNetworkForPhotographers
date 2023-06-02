using Newtonsoft.Json;
using UserClientAppNetworkForPhotographers.Exceptions;
using UserClientAppNetworkForPhotographers.Models.Data;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Photographers;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Subscriptions;
using System.Net;

namespace UserClientAppNetworkForPhotographers.API.ApiRequests
{
    public class ApiSubscriptions
    {
        public static async Task<int> GetCountSubscribers(int photographerId, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.SubscriptionsCountSubscribers}/{photographerId}", token);

            string responseMessage = await response.Content.ReadAsStringAsync();

            bool result = int.TryParse(responseMessage, out var count);

            if (result == true) return count;
            else throw new ApiException(StatusCodes.Status500InternalServerError);
        }

        public static async Task<int> GetCountSubscriptions(int photographerId, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.SubscriptionsCountSubscriptions}/{photographerId}", token);

            string responseMessage = await response.Content.ReadAsStringAsync();

            bool result = int.TryParse(responseMessage, out var count);

            if (result == true) return count;
            else throw new ApiException(StatusCodes.Status500InternalServerError);
        }

        public static async Task<List<GetPhotographerForListDto>> GetSubscribers(int photographerId, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.SubscriptionsSubscribers}/{photographerId}", token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var photographersForList = JsonConvert.DeserializeObject<List<GetPhotographerForListDto>>(responseMessage);

            if (photographersForList == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return photographersForList;
        }

        public static async Task<List<GetPhotographerForListDto>> GetSubscriptions(int photographerId, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.SubscriptionsSubscriptions}/{photographerId}", token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var photographersForList = JsonConvert.DeserializeObject<List<GetPhotographerForListDto>>(responseMessage);

            if (photographersForList == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return photographersForList;
        }



        public static async Task<bool> Check(SubscriptionDto subscriptionDto, string token)
        {
            var response = await ApiRequest.Post(ApiUrl.SubscriptionsCheck, subscriptionDto, token);

            string responseMessage = await response.Content.ReadAsStringAsync();

            bool result = bool.TryParse(responseMessage, out var check);

            if (result == true) return check;
            else throw new ApiException(StatusCodes.Status500InternalServerError);
        }

        public static async Task<Subscription> Create(SubscriptionDto subscriptionDto, string token)
        {
            var response = await ApiRequest.Post(ApiUrl.Subscriptions, subscriptionDto, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var subscription = JsonConvert.DeserializeObject<Subscription>(responseMessage);

            if (subscription == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return subscription;
        }

        public static async Task Delete(SubscriptionDto subscriptionDto, string token)
        {
            var response = await ApiRequest.DeleteWithBody(ApiUrl.Subscriptions, subscriptionDto, token);

            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new ApiException(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
