using Newtonsoft.Json;
using UserClientAppNetworkForPhotographers.Exceptions;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Contents;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Photographers;

namespace UserClientAppNetworkForPhotographers.API.ApiRequests
{
    public class ApiSearch
    {
        public static async Task<List<GetPhotographerForListDto>> Photographers(SearchDto searchDto, int part, string token)
        {
            var response = await ApiRequest.Post($"{ApiUrl.PhotographersSearch}/{part}", searchDto, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var photographers = JsonConvert.DeserializeObject<List<GetPhotographerForListDto>>(responseMessage);

            if (photographers == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return photographers;
        }

        public static async Task<List<GetContentForListDto>> Posts(SearchDto searchDto, int part, string token)
        {
            var response = await ApiRequest.Post($"{ApiUrl.ContentsPostsSearch}/{part}", searchDto, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var contents = JsonConvert.DeserializeObject<List<GetContentForListDto>>(responseMessage);

            if (contents == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return contents;
        }

        public static async Task<List<GetContentForListDto>> Blogs(SearchDto searchDto, int part, string token)
        {
            var response = await ApiRequest.Post($"{ApiUrl.ContentsBlogsSearch}/{part}", searchDto, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var contents = JsonConvert.DeserializeObject<List<GetContentForListDto>>(responseMessage);

            if (contents == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return contents;
        }
    }
}
