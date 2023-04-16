using ServerAppNetworkForPhotographers.Dtos.Complaints;
using ServerAppNetworkForPhotographers.Models;

namespace ServerAppNetworkForPhotographers.Interfaces.Services
{
    public interface IComplaintsBaseService
    {
        Task<List<ComplaintBase>> GetAllComplaintsBase();
        Task<ComplaintBase?> GetComplaintBaseById(int id);
        Task<ComplaintBase> CreateComplaintBase(CreateComplaintBaseDto newComplaintBase);
        Task<ComplaintBase> UpdateComplaintBase(UpdateComplaintBaseDto updatedComplaintBase);
        Task DeleteComplaintBase(int id);
    }
}
