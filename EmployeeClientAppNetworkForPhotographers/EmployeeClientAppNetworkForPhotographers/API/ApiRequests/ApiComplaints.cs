using EmployeeClientAppNetworkForPhotographers.Exceptions;
using EmployeeClientAppNetworkForPhotographers.Models.Data;
using EmployeeClientAppNetworkForPhotographers.Models.Data.Dtos.Complaints;
using Newtonsoft.Json;
using System.Net;

namespace EmployeeClientAppNetworkForPhotographers.API.ApiRequests
{
    public class ApiComplaints
    {
        public static async Task<List<GetPhotographerCountComplaints>> GetCountPhotographersComplaintsOpen(string token)
        {
            var response = await ApiRequest.Get(ApiUrl.ComplaintsPhotographers, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var complaints = JsonConvert.DeserializeObject<List<GetPhotographerCountComplaints>>(responseMessage);

            if (complaints == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return complaints;
        }

        public static async Task<List<GetContentWithCountComplaints>> GetPhotodrapherContentsWithCountComplaints(int photographerId, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.ComplaintsPhotographers}/{photographerId}", token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var complaints = JsonConvert.DeserializeObject<List<GetContentWithCountComplaints>>(responseMessage);

            if (complaints == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return complaints;
        }

        public static async Task<List<Complaint>> GetComplaintsOpenForContent(int contentId, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.ComplaintsContents}/{contentId}", token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var complaints = JsonConvert.DeserializeObject<List<Complaint>>(responseMessage);

            if (complaints == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return complaints;
        }

        public static async Task UpdateStatus(int id, string token)
        {
            var response = await ApiRequest.PutWithoutBody($"{ApiUrl.ComplaintsStatus}/{id}", token);

            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new ApiException(StatusCodes.Status500InternalServerError);
            }
        }

        public static async Task UpdateAllStatusForContent(int contentId, string token)
        {
            var response = await ApiRequest.PutWithoutBody($"{ApiUrl.ComplaintsStatusContent}/{contentId}", token);

            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new ApiException(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
