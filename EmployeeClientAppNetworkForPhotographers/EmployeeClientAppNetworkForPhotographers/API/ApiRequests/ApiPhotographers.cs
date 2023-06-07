using EmployeeClientAppNetworkForPhotographers.Exceptions;
using EmployeeClientAppNetworkForPhotographers.Models.Data.Dtos.Photographers;
using Newtonsoft.Json;
using System.Net;

namespace EmployeeClientAppNetworkForPhotographers.API.ApiRequests
{
    public class ApiPhotographers
    {
        public static async Task<GetPhotographerForListDto> GetById(int id, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.ForEmployeesPhotographer}/{id}", token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var photographer = JsonConvert.DeserializeObject<GetPhotographerForListDto>(responseMessage);

            if (photographer == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return photographer;
        }

        public static async Task<Stream> GetPhotoByName(string name, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.PhotographersPhoto}/{name}", token);
            return await response.Content.ReadAsStreamAsync();
        }

        public static async Task UpdateStatus(int id, string token)
        {
            var response = await ApiRequest.PutWithoutBody($"{ApiUrl.ForEmployeesPhotographerStatus}/{id}", token);

            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new ApiException(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
