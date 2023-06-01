using Newtonsoft.Json;
using System.Net;
using UserClientAppNetworkForPhotographers.Exceptions;
using UserClientAppNetworkForPhotographers.Models.Data;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Contents;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Favourites;

namespace UserClientAppNetworkForPhotographers.API.ApiRequests
{
    public class ApiFavourites
    {
        public static async Task<List<GetContentForListDto>> GetPosts(int photographerId, int part, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.ContentsPostsFavourites}/{photographerId}/{part}", token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var contents = JsonConvert.DeserializeObject<List<GetContentForListDto>>(responseMessage);

            if (contents == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return contents;
        }

        public static async Task<List<GetContentForListDto>> GetBlogs(int photographerId, int part, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.ContentsBlogsFavourites}/{photographerId}/{part}", token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var contents = JsonConvert.DeserializeObject<List<GetContentForListDto>>(responseMessage);

            if (contents == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return contents;
        }

        public static async Task<Favourite> Create(FavouriteDto favouriteDto, string token)
        {
            var response = await ApiRequest.Post(ApiUrl.Favourites, favouriteDto, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var favourite = JsonConvert.DeserializeObject<Favourite>(responseMessage);

            if (favourite == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return favourite;
        }

        public static async Task Delete(FavouriteDto favouriteDto, string token)
        {
            var response = await ApiRequest.DeleteWithBody(ApiUrl.Favourites, favouriteDto, token);

            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new ApiException(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
