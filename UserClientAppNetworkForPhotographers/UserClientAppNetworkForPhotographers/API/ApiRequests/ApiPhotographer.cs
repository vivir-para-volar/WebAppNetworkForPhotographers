using Newtonsoft.Json;
using System.Net;
using UserClientAppNetworkForPhotographers.Exceptions;
using UserClientAppNetworkForPhotographers.Models.Data;

namespace UserClientAppNetworkForPhotographers.API.ApiRequests
{
    public class ApiPhotographer
    {
        public static async Task<Photographer> GetPhotographerById(int id)
        {
            var response = await ApiRequest.Get($"{ApiUrl.Photographers}/{id}");

            string responseMessage = await response.Content.ReadAsStringAsync();
            var photographer = JsonConvert.DeserializeObject<Photographer>(responseMessage);

            if (photographer == null) throw new ApiException((int)HttpStatusCode.InternalServerError);
            return photographer;
        }
    }
}
