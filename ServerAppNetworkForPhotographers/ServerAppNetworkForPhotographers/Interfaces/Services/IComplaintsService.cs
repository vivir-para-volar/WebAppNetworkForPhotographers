using ServerAppNetworkForPhotographers.Dtos.Complaints;
using ServerAppNetworkForPhotographers.Models;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface IComplaintsService
    {
        Task<List<Complaint>> GetAllComplaints();
        Task<Complaint?> GetComplaintById(int id);
        Task<Complaint> CreateComplaint(CreateComplaintDto newComplaint);
        Task<Complaint> UpdateComplaint(UpdateComplaintDto updatedComplaint);
        Task DeleteComplaint(int id);
    }
}
