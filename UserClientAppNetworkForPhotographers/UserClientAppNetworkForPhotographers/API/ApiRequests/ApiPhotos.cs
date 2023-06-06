using Newtonsoft.Json;
using UserClientAppNetworkForPhotographers.Exceptions;
using UserClientAppNetworkForPhotographers.Models.Data;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.PhotosInfo;

namespace UserClientAppNetworkForPhotographers.API.ApiRequests
{
    public class ApiPhotos
    {
        public static async Task<Stream> GetPhotoByName(int contentId, string name, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.Photos}/{contentId}/{name}", token);
            return await response.Content.ReadAsStreamAsync();
        }

        public static async Task<Photo> Create(int contentId, IFormFile photo, string token)
        {
            var response = await ApiRequest.PutPhoto($"{ApiUrl.Photos}/{contentId}", photo, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var contentPhoto = JsonConvert.DeserializeObject<Photo>(responseMessage);

            if (contentPhoto == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return contentPhoto;
        }

        public static async Task<PhotoInfo> GetPhotoInfoByPhotoId(int id, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.PhotosInfo}/{id}", token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var photoInfo = JsonConvert.DeserializeObject<PhotoInfo>(responseMessage);

            if (photoInfo == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return photoInfo;
        }

        public static async Task<PhotoInfo> CreatePhotoInfo(CreatePhotoInfoDto photoInfoDto, string token)
        {
            var response = await ApiRequest.Post(ApiUrl.PhotosInfo, photoInfoDto, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var photoInfo = JsonConvert.DeserializeObject<PhotoInfo>(responseMessage);

            if (photoInfo == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return photoInfo;
        }
    }
}
