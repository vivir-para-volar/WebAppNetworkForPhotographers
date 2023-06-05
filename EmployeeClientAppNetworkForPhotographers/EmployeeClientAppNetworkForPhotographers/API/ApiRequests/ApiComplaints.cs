using Newtonsoft.Json;
using EmployeeClientAppNetworkForPhotographers.Exceptions;
using EmployeeClientAppNetworkForPhotographers.Models.Data;

namespace EmployeeClientAppNetworkForPhotographers.API.ApiRequests
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
    }
}
