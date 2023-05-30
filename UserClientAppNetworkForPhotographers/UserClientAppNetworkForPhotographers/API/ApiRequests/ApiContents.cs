using Newtonsoft.Json;
using System.Net;
using UserClientAppNetworkForPhotographers.Exceptions;
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
