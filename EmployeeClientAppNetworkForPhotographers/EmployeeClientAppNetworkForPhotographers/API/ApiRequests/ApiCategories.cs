using EmployeeClientAppNetworkForPhotographers.Exceptions;
using EmployeeClientAppNetworkForPhotographers.Models.Data.Dtos.CategoryDirs;
using Newtonsoft.Json;

namespace EmployeeClientAppNetworkForPhotographers.API.ApiRequests
{
    public class ApiCategories
    {
        //public static async Task<List<GetCategoryDirDto>> GetAllWithDirs(string token)
        //{
        //    var response = await ApiRequest.Get(ApiUrl.CategoryDirsWithCategories, token);

        //    string responseMessage = await response.Content.ReadAsStringAsync();
        //    var categoryDirs = JsonConvert.DeserializeObject<List<GetCategoryDirDto>>(responseMessage);

        //    if (categoryDirs == null) throw new ApiException(StatusCodes.Status500InternalServerError);
        //    return categoryDirs;
        //}
    }
}
