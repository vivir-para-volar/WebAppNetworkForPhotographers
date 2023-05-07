using ServerAppNetworkForPhotographers.Models;
using ServerAppNetworkForPhotographers.Models.Dtos.ComplaintsBase;

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
