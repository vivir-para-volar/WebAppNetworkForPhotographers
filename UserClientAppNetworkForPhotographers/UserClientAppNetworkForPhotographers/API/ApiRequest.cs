using Newtonsoft.Json;
using ServerAppNetworkForPhotographers.Exceptions;
using System.Net;
using System.Text;
using UserClientAppNetworkForPhotographers.Exceptions;
using UserClientAppNetworkForPhotographers.Models.ExceptionsResponses;

namespace UserClientAppNetworkForPhotographers.API
{
    public static class ApiRequest
    {
        private static readonly HttpClient _client = new HttpClient();

        private static string GetToken()
        {
            var token = AppUser.Token;
            if (token == null) throw new ApiException((int)HttpStatusCode.InternalServerError);

            return token;
        }

        public static async Task<HttpResponseMessage> Get(string url)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get
            };

            request.Headers.Add("Authorization", "Bearer " + GetToken());

            HttpResponseMessage response = await _client.SendAsync(request);
            if (!response.IsSuccessStatusCode) await ProcessException(response);

            return response;
        }

        public static async Task<HttpResponseMessage> Post(string url, Object objectToSend)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Post
            };

            request.Headers.Add("Authorization", "Bearer " + GetToken());

            request.Content = new StringContent(
                JsonConvert.SerializeObject(objectToSend),
                Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage response = await _client.SendAsync(request);
            if (!response.IsSuccessStatusCode) await ProcessException(response);

            return response;
        }

        public static async Task<HttpResponseMessage> PostWithoutAuthorization(string url, Object objectToSend)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Post
            };

            request.Content = new StringContent(
                JsonConvert.SerializeObject(objectToSend),
                Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage response = await _client.SendAsync(request);
            if (!response.IsSuccessStatusCode) await ProcessException(response);

            return response;
        }

        public static async Task<HttpResponseMessage> Put(string url, Object objectToSend)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Put
            };

            request.Headers.Add("Authorization", "Bearer " + GetToken());

            request.Content = new StringContent(
                JsonConvert.SerializeObject(objectToSend),
                Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage response = await _client.SendAsync(request);
            if (!response.IsSuccessStatusCode) await ProcessException(response);

            return response;
        }

        public static async Task<HttpResponseMessage> Delete(string url)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Delete
            };

            request.Headers.Add("Authorization", "Bearer " + GetToken());

            HttpResponseMessage response = await _client.SendAsync(request);
            if (!response.IsSuccessStatusCode) await ProcessException(response);

            return response;
        }

        public static async Task ProcessException(HttpResponseMessage response)
        {
            string responseMessage = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                var uniqueFieldResponse = JsonConvert.DeserializeObject<UniqueFieldResponse>(responseMessage);
                if (uniqueFieldResponse != null)
                    throw new UniqueFieldException(uniqueFieldResponse.Field);
            }

            var exceptionResponse = JsonConvert.DeserializeObject<ExceptionResponse>(responseMessage);
            if (exceptionResponse != null)
                throw new ApiException(exceptionResponse.Status, exceptionResponse.Message);

            throw new ApiException((int)HttpStatusCode.InternalServerError);
        }
    }
}