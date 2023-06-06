using Newtonsoft.Json;
using EmployeeClientAppNetworkForPhotographers.Exceptions;
using EmployeeClientAppNetworkForPhotographers.Models.Data.Dtos.CategoryDirs;
using EmployeeClientAppNetworkForPhotographers.Models.Data;
using System.Net;

namespace EmployeeClientAppNetworkForPhotographers.API.ApiRequests
{
    public class ApiCategoryDirs
    {
        public static async Task<List<CategoryDir>> GetAll(string token)
        {
            var response = await ApiRequest.Get(ApiUrl.CategoryDirs, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var categoryDirs = JsonConvert.DeserializeObject<List<CategoryDir>>(responseMessage);

            if (categoryDirs == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return categoryDirs;
        }

        public static async Task<List<GetCategoryDirDto>> GetAllWithDirs(string token)
        {
            var response = await ApiRequest.Get(ApiUrl.CategoryDirsWithCategories, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var categoryDirs = JsonConvert.DeserializeObject<List<GetCategoryDirDto>>(responseMessage);

            if (categoryDirs == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return categoryDirs;
        }

        public static async Task<CategoryDir> GetById(int id, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.CategoryDirs}/{id}", token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var categoryDir = JsonConvert.DeserializeObject<CategoryDir>(responseMessage);

            if (categoryDir == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return categoryDir;
        }

        public static async Task<bool> CheckCategories(int id, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.CategoryDirsCheckCategories}/{id}", token);

            string responseMessage = await response.Content.ReadAsStringAsync();

            bool result = bool.TryParse(responseMessage, out var check);

            if (result == true) return check;
            else throw new ApiException(StatusCodes.Status500InternalServerError);
        }

        public static async Task<CategoryDir> Create(CreateCategoryDirDto categoryDirDto, string token)
        {
            var response = await ApiRequest.Post(ApiUrl.CategoryDirs, categoryDirDto, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var categoryDir = JsonConvert.DeserializeObject<CategoryDir>(responseMessage);

            if (categoryDir == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return categoryDir;
        }

        public static async Task<CategoryDir> Update(CategoryDir categoryDirDto, string token)
        {
            var response = await ApiRequest.Put(ApiUrl.CategoryDirs, categoryDirDto, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var categoryDir = JsonConvert.DeserializeObject<CategoryDir>(responseMessage);

            if (categoryDir == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return categoryDir;
        }

        public static async Task Delete(int id, string token)
        {
            var response = await ApiRequest.Delete($"{ApiUrl.CategoryDirs}/{id}", token);

            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new ApiException(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
