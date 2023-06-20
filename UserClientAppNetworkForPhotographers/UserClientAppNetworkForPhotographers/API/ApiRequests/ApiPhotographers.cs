using Newtonsoft.Json;
using System.Net;
using UserClientAppNetworkForPhotographers.Exceptions;
using UserClientAppNetworkForPhotographers.Models.Data;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Photographers;

namespace UserClientAppNetworkForPhotographers.API.ApiRequests
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

        public static async Task<PhotographerInfo> GetInfoByPhotographerId(int photographerId, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.PhotographersInfo}/{photographerId}", token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var photographerInfo = JsonConvert.DeserializeObject<PhotographerInfo>(responseMessage);

            if (photographerInfo == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return photographerInfo;
        }

        public static async Task<Photographer> Update(UpdatePhotographerDto photographerDto, string token)
        {
            var response = await ApiRequest.Put(ApiUrl.Photographers, photographerDto, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var photographer = JsonConvert.DeserializeObject<Photographer>(responseMessage);

            if (photographer == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return photographer;
        }

        public static async Task<Photographer> UpdatePhotoProfile(int id, IFormFile photo, string token)
        {
            var response = await ApiRequest.PutPhoto($"{ApiUrl.PhotographersPhoto}/{id}", photo, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var photographer = JsonConvert.DeserializeObject<Photographer>(responseMessage);

            if (photographer == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return photographer;
        }

        public static async Task<PhotographerInfo> UpdateInfo(UpdatePhotographerInfoDto photographerInfoDto, string token)
        {
            var response = await ApiRequest.Put(ApiUrl.PhotographersInfo, photographerInfoDto, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var photographerInfo = JsonConvert.DeserializeObject<PhotographerInfo>(responseMessage);

            if (photographerInfo == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return photographerInfo;
        }

        public static async Task DeletePhotoProfile(int id, string token)
        {
            var response = await ApiRequest.Delete($"{ApiUrl.PhotographersPhoto}/{id}", token);

            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new ApiException(StatusCodes.Status500InternalServerError);
            }
        }

        public static async Task Delete(int id, string token)
        {
            var response = await ApiRequest.Delete($"{ApiUrl.Photographers}/{id}", token);

            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new ApiException(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
