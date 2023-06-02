using Newtonsoft.Json;
using UserClientAppNetworkForPhotographers.Exceptions;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Contents;

namespace UserClientAppNetworkForPhotographers.API.ApiRequests
{
    public class ApiContentsProfile
    {
        public static async Task<List<GetContentForListDto>> GetUserPosts(int photographerId, int part, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.ContentsPostsUser}/{photographerId}/{part}", token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var contents = JsonConvert.DeserializeObject<List<GetContentForListDto>>(responseMessage);

            if (contents == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return contents;
        }

        public static async Task<List<GetContentForListDto>> GetUserBlogs(int photographerId, int part, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.ContentsBlogsUser}/{photographerId}/{part}", token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var contents = JsonConvert.DeserializeObject<List<GetContentForListDto>>(responseMessage);

            if (contents == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return contents;
        }

        public static async Task<List<GetContentForListDto>> GetPhotographerPosts(int photographerId, int part, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.ContentsPostsPhotographer}/{photographerId}/{part}", token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var contents = JsonConvert.DeserializeObject<List<GetContentForListDto>>(responseMessage);

            if (contents == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return contents;
        }

        public static async Task<List<GetContentForListDto>> GetPhotographerBlogs(int photographerId, int part, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.ContentsBlogsPhotographer}/{photographerId}/{part}", token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var contents = JsonConvert.DeserializeObject<List<GetContentForListDto>>(responseMessage);

            if (contents == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return contents;
        }
    }
}
