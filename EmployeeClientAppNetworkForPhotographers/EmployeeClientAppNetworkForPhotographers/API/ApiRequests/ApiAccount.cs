using EmployeeClientAppNetworkForPhotographers.Exceptions;
using EmployeeClientAppNetworkForPhotographers.Models.Account;
using EmployeeClientAppNetworkForPhotographers.Models.Account.Dtos;
using Newtonsoft.Json;
using System.Net;

namespace EmployeeClientAppNetworkForPhotographers.API.ApiRequests
{
    public static class ApiAccount
    {
        public static async Task Register(UserRegister userRegister)
        {
            await ApiRequest.PostWithoutAuthorization(ApiUrl.RegisterUser, userRegister);
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
    }
}
