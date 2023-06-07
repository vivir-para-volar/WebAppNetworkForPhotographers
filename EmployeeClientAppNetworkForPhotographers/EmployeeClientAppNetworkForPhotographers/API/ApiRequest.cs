using EmployeeClientAppNetworkForPhotographers.Exceptions;
using EmployeeClientAppNetworkForPhotographers.Models.ExceptionsResponses;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace EmployeeClientAppNetworkForPhotographers.API
{
    public static class ApiRequest
    {
        private static readonly HttpClient _client = new HttpClient();

        public static async Task<HttpResponseMessage> Get(string url, string token)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get
            };

            request.Headers.Add("Authorization", "Bearer " + token);

            HttpResponseMessage response = await _client.SendAsync(request);
            if (!response.IsSuccessStatusCode) await ProcessException(response);
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                throw new ApiException(StatusCodes.Status404NotFound, HttpStatusCode.NotFound.ToString());
            }

            return response;
        }

        public static async Task<HttpResponseMessage> Post(string url, Object objectToSend, string token)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Post
            };

            request.Headers.Add("Authorization", "Bearer " + token);

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

        public static async Task<HttpResponseMessage> Put(string url, Object objectToSend, string token)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Put
            };

            request.Headers.Add("Authorization", "Bearer " + token);

            request.Content = new StringContent(
                JsonConvert.SerializeObject(objectToSend),
                Encoding.UTF8,
                "application/json"
            );

            HttpResponseMessage response = await _client.SendAsync(request);
            if (!response.IsSuccessStatusCode) await ProcessException(response);

            return response;
        }

        public static async Task<HttpResponseMessage> PutWithoutBody(string url, string token)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Put
            };

            request.Headers.Add("Authorization", "Bearer " + token);

            HttpResponseMessage response = await _client.SendAsync(request);
            if (!response.IsSuccessStatusCode) await ProcessException(response);

            return response;
        }

        public static async Task<HttpResponseMessage> Delete(string url, string token)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Delete
            };

            request.Headers.Add("Authorization", "Bearer " + token);

            HttpResponseMessage response = await _client.SendAsync(request);
            if (!response.IsSuccessStatusCode) await ProcessException(response);

            return response;
        }

        public static async Task ProcessException(HttpResponseMessage response)
        {
            string responseMessage = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                var uniqueFieldResponse = JsonConvert.DeserializeObject<FieldResponse>(responseMessage);
                if (uniqueFieldResponse != null)
                    throw new FieldException(uniqueFieldResponse.Field);
            }

            var exceptionResponse = JsonConvert.DeserializeObject<ExceptionResponse>(responseMessage);
            if (exceptionResponse != null)
                throw new ApiException(exceptionResponse.Status, exceptionResponse.Message);

            throw new ApiException((int)response.StatusCode, response.StatusCode.ToString());
        }
    }
}