using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Dtos.ComplaintsBase;

namespace ServerAppNetworkForPhotographers.Interfaces.Controllers
{
    public interface IComplaintsBaseController
    {
        Task<ActionResult<List<ComplaintBase>>> GetAllComplaintsBase();
        Task<ActionResult<ComplaintBase?>> GetComplaintBaseById(int id);
        Task<ActionResult<ComplaintBase>> CreateComplaintBase(CreateComplaintBaseDto complaintBaseDto);
        Task<ActionResult<ComplaintBase>> UpdateComplaintBase(UpdateComplaintBaseDto complaintBaseDto);
        Task<ActionResult> DeleteComplaintBase(int id);
    }
}
