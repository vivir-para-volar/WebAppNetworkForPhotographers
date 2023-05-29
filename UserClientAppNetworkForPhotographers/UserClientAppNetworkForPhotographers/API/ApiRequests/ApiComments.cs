using Newtonsoft.Json;
using System.Net;
using UserClientAppNetworkForPhotographers.Exceptions;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Comments;

namespace UserClientAppNetworkForPhotographers.API.ApiRequests
{
    public class ApiComments
    {
        public static async Task<List<GetCommentDto>> GetAllForContent(int contentId, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.CommentsContent}/{contentId}", token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var comments = JsonConvert.DeserializeObject<List<GetCommentDto>>(responseMessage);

            if (comments == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return comments;
        }

        public static async Task<GetCommentDto> Create(CreateCommentDto commentDto, string token)
        {
            var response = await ApiRequest.Post(ApiUrl.Comments, commentDto, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var comment = JsonConvert.DeserializeObject<GetCommentDto>(responseMessage);

            if (comment == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return comment;
        }

        public static async Task Delete(int id, string token)
        {
            var response = await ApiRequest.Delete($"{ApiUrl.Comments}/{id}", token);

            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new ApiException(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
