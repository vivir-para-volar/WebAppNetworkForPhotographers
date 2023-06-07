using EmployeeClientAppNetworkForPhotographers.Exceptions;
using EmployeeClientAppNetworkForPhotographers.Models.Data.Dtos.Contents;
using Newtonsoft.Json;
using System.Net;

namespace EmployeeClientAppNetworkForPhotographers.API.ApiRequests
{
    public class ApiContents
    {
        public static async Task<GetContentForEmployeeDto> GetById(int id, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.ForEmployeesContent}/{id}", token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<GetContentForEmployeeDto>(responseMessage);

            if (content == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return content;
        }

        public static async Task<Stream> GetBlogMainPhotoByName(string name, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.ContentsBlogsMainPhoto}/{name}", token);
            return await response.Content.ReadAsStreamAsync();
        }

        public static async Task<Stream> GetPhotoByName(int contentId, string name, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.Photos}/{contentId}/{name}", token);
            return await response.Content.ReadAsStreamAsync();
        }

        public static async Task UpdateStatus(int id, string token)
        {
            var response = await ApiRequest.PutWithoutBody($"{ApiUrl.ForEmployeesContentStatus}/{id}", token);

            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new ApiException(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
