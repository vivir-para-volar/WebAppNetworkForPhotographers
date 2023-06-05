using EmployeeClientAppNetworkForPhotographers.Exceptions;
using EmployeeClientAppNetworkForPhotographers.Models.Data;
using Newtonsoft.Json;

namespace EmployeeClientAppNetworkForPhotographers.API.ApiRequests
{
    public class ApiPhotographers
    {
        public static async Task<Photographer> GetById(int id, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.Photographers}/{id}", token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var photographer = JsonConvert.DeserializeObject<Photographer>(responseMessage);

            if (photographer == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return photographer;
        }

        public static async Task<Stream> GetPhotoByName(string name, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.PhotographersPhoto}/{name}", token);
            return await response.Content.ReadAsStreamAsync();
        }
    }
}
