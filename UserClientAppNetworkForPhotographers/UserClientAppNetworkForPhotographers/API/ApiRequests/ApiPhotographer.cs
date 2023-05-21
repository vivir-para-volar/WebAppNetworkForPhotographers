using Newtonsoft.Json;
using System.Net;
using UserClientAppNetworkForPhotographers.Exceptions;
using UserClientAppNetworkForPhotographers.Models.Data;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Photographers;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.PhotographersInfo;

namespace UserClientAppNetworkForPhotographers.API.ApiRequests
{
    public class ApiPhotographer
    {
        public static async Task<Photographer> GetById(int id, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.Photographers}/{id}", token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var photographer = JsonConvert.DeserializeObject<Photographer>(responseMessage);

            if (photographer == null) throw new ApiException((int)HttpStatusCode.InternalServerError);
            return photographer;
        }

        public static async Task<PhotographerInfo> GetInfoByPhotographerId(int photographerId, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.PhotographersInfo}/{photographerId}", token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var photographerInfo = JsonConvert.DeserializeObject<PhotographerInfo>(responseMessage);

            if (photographerInfo == null) throw new ApiException((int)HttpStatusCode.InternalServerError);
            return photographerInfo;
        }

        public static async Task<List<GetPhotographerForListDto>> Search(SearchDto searchDto, string token)
        {
            var response = await ApiRequest.Post(ApiUrl.PhotographersSearch, searchDto, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var listPhotographers = JsonConvert.DeserializeObject<List<GetPhotographerForListDto>>(responseMessage);

            if (listPhotographers == null) throw new ApiException((int)HttpStatusCode.InternalServerError);
            return listPhotographers;
        }

        public static async Task<Photographer> Update(UpdatePhotographerDto photographerDto, string token)
        {
            var response = await ApiRequest.Put(ApiUrl.Photographers, photographerDto, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var photographer = JsonConvert.DeserializeObject<Photographer>(responseMessage);

            if (photographer == null) throw new ApiException((int)HttpStatusCode.InternalServerError);
            return photographer;
        }

        public static async Task<Photographer> UpdateProfilePhoto(int id, IFormFile photo, string token)
        {
            var response = await ApiRequest.Put($"{ApiUrl.Photographers}/{id}", photo, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var photographer = JsonConvert.DeserializeObject<Photographer>(responseMessage);

            if (photographer == null) throw new ApiException((int)HttpStatusCode.InternalServerError);
            return photographer;
        }

        public static async Task<PhotographerInfo> UpdateInfo(UpdatePhotographerInfoDto photographerInfoDto, string token)
        {
            var response = await ApiRequest.Put(ApiUrl.PhotographersInfo, photographerInfoDto, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var photographerInfo = JsonConvert.DeserializeObject<PhotographerInfo>(responseMessage);

            if (photographerInfo == null) throw new ApiException((int)HttpStatusCode.InternalServerError);
            return photographerInfo;
        }

        public static async Task Delete(int id, string token)
        {
            var response = await ApiRequest.Delete($"{ApiUrl.Photographers}/{id}", token);

            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new ApiException((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
