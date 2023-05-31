using Newtonsoft.Json;
using UserClientAppNetworkForPhotographers.Models.Data.Dtos.Complaints;
using UserClientAppNetworkForPhotographers.Exceptions;
using UserClientAppNetworkForPhotographers.Models.Data;

namespace UserClientAppNetworkForPhotographers.API.ApiRequests
{
    public class ApiComplaints
    {
        public static async Task<List<ComplaintBase>> GetAllBase(string token)
        {
            var response = await ApiRequest.Get(ApiUrl.ComplaintsBase, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var complaintsBase = JsonConvert.DeserializeObject<List<ComplaintBase>>(responseMessage);

            if (complaintsBase == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return complaintsBase;
        }

        public static async Task<Complaint> Create(CreateComplaintDto complaintDto, string token)
        {
            var response = await ApiRequest.Post(ApiUrl.Complaints, complaintDto, token);

            string responseMessage = await response.Content.ReadAsStringAsync();
            var complaint = JsonConvert.DeserializeObject<Complaint>(responseMessage);

            if (complaint == null) throw new ApiException(StatusCodes.Status500InternalServerError);
            return complaint;
        }
    }
}
