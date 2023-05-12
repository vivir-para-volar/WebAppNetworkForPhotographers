using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Complaints;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface IComplaintsService
    {
        Task<List<Complaint>> GetAllComplaintsOpen();
        Task<Complaint?> GetComplaintById(int id);
        Task<Complaint> CreateComplaint(CreateComplaintDto complaintDto);
        Task<Complaint> UpdateComplaintStatus(int id);
    }
}
