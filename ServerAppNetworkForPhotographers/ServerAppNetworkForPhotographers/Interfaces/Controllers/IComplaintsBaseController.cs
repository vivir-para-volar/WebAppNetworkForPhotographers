using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Dtos.Complaints;
using ServerAppNetworkForPhotographers.Models;

namespace ServerAppNetworkForPhotographers.Interfaces.Controllers
{
    public interface IComplaintsBaseController
    {
        Task<ActionResult<List<ComplaintBase>>> GetAllComplaintsBase();
        Task<ActionResult<ComplaintBase>> GetComplaintBaseById(int id);
        Task<ActionResult<ComplaintBase>> CreateComplaintBase(CreateComplaintBaseDto newComplaintBase);
        Task<ActionResult<ComplaintBase>> UpdateComplaintBase(UpdateComplaintBaseDto updatedComplaintBase);
        Task<ActionResult> DeleteComplaintBase(int id);
    }
}
