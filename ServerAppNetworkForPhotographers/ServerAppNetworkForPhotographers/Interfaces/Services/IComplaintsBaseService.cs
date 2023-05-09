using ServerAppNetworkForPhotographers.Models;
using ServerAppNetworkForPhotographers.Models.Dtos.ComplaintsBase;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface IComplaintsBaseService
    {
        Task<List<ComplaintBase>> GetAllComplaintsBase();
        Task<ComplaintBase?> GetComplaintBaseById(int id);
        Task<ComplaintBase> CreateComplaintBase(CreateComplaintBaseDto complaintBaseDto);
        Task<ComplaintBase> UpdateComplaintBase(UpdateComplaintBaseDto complaintBaseDto);
        Task DeleteComplaintBase(int id);
    }
}
