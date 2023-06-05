﻿using Newtonsoft.Json;
using EmployeeClientAppNetworkForPhotographers.Exceptions;
using EmployeeClientAppNetworkForPhotographers.Models.Data.Dtos.Contents;

namespace EmployeeClientAppNetworkForPhotographers.API.ApiRequests
{
    public class ApiContents
    {
        public static async Task<GetContentDto> GetById(int id, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.Contents}/{id}", token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<GetContentDto>(responseMessage);

            if (content == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return content;
        }

        public static async Task<Stream> GetBlogMainPhotoByName(string name, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.ContentsBlogsMainPhoto}/{name}", token);
            return await response.Content.ReadAsStreamAsync();
        }
    }
}