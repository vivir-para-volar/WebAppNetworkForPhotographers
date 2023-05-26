using Newtonsoft.Json;
using System.Net;
using UserClientAppNetworkForPhotographers.Exceptions;
using UserClientAppNetworkForPhotographers.Models.Data;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Likes;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Photographers;

namespace UserClientAppNetworkForPhotographers.API.ApiRequests
{
    public class ApiLikes
    {
        public static async Task<List<GetPhotographerForListDto>> GetAllForContent(int contentId, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.LikesContent}/{contentId}", token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var photographersForList = JsonConvert.DeserializeObject<List<GetPhotographerForListDto>>(responseMessage);

            if (photographersForList == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return photographersForList;
        }

        public static async Task<Like> Create(LikeDto likeDto, string token)
        {
            var response = await ApiRequest.Post(ApiUrl.Likes, likeDto, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var like = JsonConvert.DeserializeObject<Like>(responseMessage);

            if (like == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return like;
        }

        public static async Task Delete(LikeDto likeDto, string token)
        {
            var response = await ApiRequest.DeleteWithBody(ApiUrl.Likes, likeDto, token);

            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new ApiException(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
