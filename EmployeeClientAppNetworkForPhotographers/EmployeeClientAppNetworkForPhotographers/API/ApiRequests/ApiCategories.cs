using EmployeeClientAppNetworkForPhotographers.Exceptions;
using EmployeeClientAppNetworkForPhotographers.Models.Data;
using EmployeeClientAppNetworkForPhotographers.Models.Data.Dtos.Categories;
using Newtonsoft.Json;
using System.Net;

namespace EmployeeClientAppNetworkForPhotographers.API.ApiRequests
{
    public class ApiCategories
    {
        public static async Task<GetCategoryDto> GetById(int id, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.Categories}/{id}", token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var category = JsonConvert.DeserializeObject<GetCategoryDto>(responseMessage);

            if (category == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return category;
        }

        public static async Task<bool> CheckContents(int id, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.CategoriesCheckContents}/{id}", token);

            string responseMessage = await response.Content.ReadAsStringAsync();

            bool result = bool.TryParse(responseMessage, out var check);

            if (result == true) return check;
            else throw new ApiException(StatusCodes.Status500InternalServerError);
        }

        public static async Task<GetCategoryDto> Create(CreateCategoryDto categoryDto, string token)
        {
            var response = await ApiRequest.Post(ApiUrl.Categories, categoryDto, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var category = JsonConvert.DeserializeObject<GetCategoryDto>(responseMessage);

            if (category == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return category;
        }

        public static async Task<GetCategoryDto> Update(Category categoryDto, string token)
        {
            var response = await ApiRequest.Put(ApiUrl.Categories, categoryDto, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var category = JsonConvert.DeserializeObject<GetCategoryDto>(responseMessage);

            if (category == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return category;
        }

        public static async Task Delete(int id, string token)
        {
            var response = await ApiRequest.Delete($"{ApiUrl.Categories}/{id}", token);

            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new ApiException(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
