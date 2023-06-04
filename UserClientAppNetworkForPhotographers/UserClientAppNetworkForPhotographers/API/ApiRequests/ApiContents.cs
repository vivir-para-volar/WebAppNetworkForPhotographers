using Newtonsoft.Json;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Contents;
using System.Net;
using UserClientAppNetworkForPhotographers.Exceptions;
using UserClientAppNetworkForPhotographers.Models.Data;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Contents;

namespace UserClientAppNetworkForPhotographers.API.ApiRequests
{
    public class ApiContents
    {
        public static async Task<GetContentDto> GetById(int id, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.Contents}/{id}", token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<GetContentDto>(responseMessage);

            if (content == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return content;
        }

        public static async Task<Stream> GetBlogMainPhotoByName(string name, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.ContentsBlogsMainPhoto}/{name}", token);
            return await response.Content.ReadAsStreamAsync();
        }

        public static async Task<List<GetContentForListDto>> GetNews(NewsDto newsDto, int part, string token)
        {
            var response = await ApiRequest.Post($"{ApiUrl.ContentsNews}/{part}", newsDto, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var contents = JsonConvert.DeserializeObject<List<GetContentForListDto>>(responseMessage);

            if (contents == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return contents;
        }

        public static async Task<List<GetContentForListDto>> GetOthers(OthersDto othersDto, int part, string token)
        {
            var response = await ApiRequest.Post($"{ApiUrl.ContentsOthers}/{part}", othersDto, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var contents = JsonConvert.DeserializeObject<List<GetContentForListDto>>(responseMessage);

            if (contents == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return contents;
        }

        public static async Task<Content> CreatePost(CreateContentPostDto contentPostDto, string token)
        {
            var response = await ApiRequest.Post(ApiUrl.ContentsPosts, contentPostDto, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<Content>(responseMessage);

            if (content == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return content;
        }

        public static async Task<Content> CreateBlog(CreateContentBlogDto contentBlogDto, string token)
        {
            var response = await ApiRequest.Post(ApiUrl.ContentsBlogs, contentBlogDto, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<Content>(responseMessage);

            if (content == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return content;
        }

        public static async Task<Content> UpdateBlogMainPhoto(int id, IFormFile photo, string token)
        {
            var response = await ApiRequest.PutPhoto($"{ApiUrl.ContentsBlogsMainPhoto}/{id}", photo, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<Content>(responseMessage);

            if (content == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return content;
        }

        public static async Task Delete(int id, string token)
        {
            var response = await ApiRequest.Delete($"{ApiUrl.Contents}/{id}", token);

            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new ApiException(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
