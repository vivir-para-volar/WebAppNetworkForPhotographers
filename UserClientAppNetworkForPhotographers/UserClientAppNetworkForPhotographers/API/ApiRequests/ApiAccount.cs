﻿using Newtonsoft.Json;
using System.Net;
using UserClientAppNetworkForPhotographers.Exceptions;
using UserClientAppNetworkForPhotographers.Models.Account;
using UserClientAppNetworkForPhotographers.Models.Account.Dtos;

namespace UserClientAppNetworkForPhotographers.API.ApiRequests
{
    public static class ApiAccount
    {
        public static async Task Register(UserRegisterDto userRegister)
        {
            await ApiRequest.PostWithoutAuthorization(ApiUrl.RegisterUser, userRegister);
        }

        public static async Task<TokenDto> Login(UserLogin userLogin)
        {
            var response = await ApiRequest.PostWithoutAuthorization(ApiUrl.Login, userLogin);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var tokenDto = JsonConvert.DeserializeObject<TokenDto>(responseMessage);

            if (tokenDto == null) throw new ApiException((int)HttpStatusCode.InternalServerError);

            return tokenDto;
        }
    }
}
