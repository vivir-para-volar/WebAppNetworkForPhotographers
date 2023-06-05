using System.Net;
using Newtonsoft.Json;
using EmployeeClientAppNetworkForPhotographers.Exceptions;
using EmployeeClientAppNetworkForPhotographers.Models.Account;
using EmployeeClientAppNetworkForPhotographers.Models.Account.Dtos;

namespace EmployeeClientAppNetworkForPhotographers.API.ApiRequests
{
    public static class ApiAccount
    {
        public static async Task<List<AppUserDto>> GetAllAppUsers(string token)
        {
            var response = await ApiRequest.Get(ApiUrl.Account, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var appUsers = JsonConvert.DeserializeObject<List<AppUserDto>>(responseMessage);

            if (appUsers == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return appUsers;
        }

        public static async Task<AppUserDto> GetAppUserById(string id, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.Account}/{id}", token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var appUser = JsonConvert.DeserializeObject<AppUserDto>(responseMessage);

            if (appUser == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return appUser;
        }

        public static async Task<AppUserDto> RegisterEmployee(UserRegister userRegister, string token)
        {
            var response = await ApiRequest.Post(ApiUrl.RegisterEmployee, userRegister, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var appUser = JsonConvert.DeserializeObject<AppUserDto>(responseMessage);

            if (appUser == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return appUser;
        }

        public static async Task<AppUserDto> RegisterAdmin(UserRegister userRegister, string token)
        {
            var response = await ApiRequest.Post(ApiUrl.RegisterAdmin, userRegister, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var appUser = JsonConvert.DeserializeObject<AppUserDto>(responseMessage);

            if (appUser == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return appUser;
        }

        public static async Task<TokenDto> Login(UserLogin userLogin)
        {
            var response = await ApiRequest.PostWithoutAuthorization(ApiUrl.Login, userLogin);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var tokenDto = JsonConvert.DeserializeObject<TokenDto>(responseMessage);

            if (tokenDto == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return tokenDto;
        }

        public static async Task UpdatePassword(UpdatePassword updatePassword, string token)
        {
            var response = await ApiRequest.Put($"{ApiUrl.UpdatePassword}", updatePassword, token);

            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new ApiException(StatusCodes.Status500InternalServerError);
            }
        }

        public static async Task<AppUserDto> UpdateAppUser(AppUserDto appUserDto, string token)
        {
            var response = await ApiRequest.Put(ApiUrl.Account, appUserDto, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var appUser = JsonConvert.DeserializeObject<AppUserDto>(responseMessage);

            if (appUser == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return appUser;
        }

        public static async Task DeleteAppUser(string id, string token)
        {
            var response = await ApiRequest.Delete($"{ApiUrl.Account}/{id}", token);

            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new ApiException(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
