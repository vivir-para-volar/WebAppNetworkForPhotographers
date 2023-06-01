using Newtonsoft.Json;
using UserClientAppNetworkForPhotographers.Exceptions;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.CategoryDirs;

namespace UserClientAppNetworkForPhotographers.API.ApiRequests
{
    public class ApiCategories
    {
        public static async Task<List<GetCategoryDirDto>> GetAllWithDirs(string token)
        {
            var response = await ApiRequest.Get(ApiUrl.CategoryDirsWithCategories, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var categoryDirs = JsonConvert.DeserializeObject<List<GetCategoryDirDto>>(responseMessage);

            if (categoryDirs == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return categoryDirs;
        }
    }
}
