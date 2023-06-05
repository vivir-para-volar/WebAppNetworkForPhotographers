using EmployeeClientAppNetworkForPhotographers.Exceptions;
using EmployeeClientAppNetworkForPhotographers.Models.Data;
using EmployeeClientAppNetworkForPhotographers.Models.Data.Dtos.ComplaintsBase;
using Newtonsoft.Json;
using System.Net;

namespace EmployeeClientAppNetworkForPhotographers.API.ApiRequests
{
    public class ApiComplaintsBase
    {
        public static async Task<List<ComplaintBase>> GetAll(string token)
        {
            var response = await ApiRequest.Get(ApiUrl.ComplaintsBase, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var complaintsBase = JsonConvert.DeserializeObject<List<ComplaintBase>>(responseMessage);

            if (complaintsBase == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return complaintsBase;
        }

        public static async Task<ComplaintBase> GetById(int id, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.ComplaintsBase}/{id}", token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var complaintBase = JsonConvert.DeserializeObject<ComplaintBase>(responseMessage);

            if (complaintBase == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return complaintBase;
        }

        public static async Task<bool> CheckComplaints(int id, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.ComplaintsBaseCheckComplaints}/{id}", token);

            string responseMessage = await response.Content.ReadAsStringAsync();

            bool result = bool.TryParse(responseMessage, out var check);

            if (result == true) return check;
            else throw new ApiException(StatusCodes.Status500InternalServerError);
        }

        public static async Task<ComplaintBase> Create(CreateComplaintBaseDto complaintBaseDto, string token)
        {
            var response = await ApiRequest.Post(ApiUrl.ComplaintsBase, complaintBaseDto, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var complaintBase = JsonConvert.DeserializeObject<ComplaintBase>(responseMessage);

            if (complaintBase == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return complaintBase;
        }

        public static async Task<ComplaintBase> Update(ComplaintBase complaintBaseDto, string token)
        {
            var response = await ApiRequest.Put(ApiUrl.ComplaintsBase, complaintBaseDto, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var complaintBase = JsonConvert.DeserializeObject<ComplaintBase>(responseMessage);

            if (complaintBase == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return complaintBase;
        }

        public static async Task Delete(int id, string token)
        {
            var response = await ApiRequest.Delete($"{ApiUrl.ComplaintsBase}/{id}", token);

            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new ApiException(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
