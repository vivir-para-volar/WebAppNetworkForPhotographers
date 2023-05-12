using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Complaints;

namespace ServerAppNetworkForPhotographers.Interfaces.Controllers
{
    public interface IComplaintsController
    {
        Task<ActionResult<List<Complaint>>> GetAllComplaintsOpen();
        Task<ActionResult<Complaint?>> GetComplaintById(int id);
        Task<ActionResult<Complaint>> CreateComplaint(CreateComplaintDto complaintDto);
        Task<ActionResult<Complaint>> UpdateComplaintStatus(int id);
    }
}
