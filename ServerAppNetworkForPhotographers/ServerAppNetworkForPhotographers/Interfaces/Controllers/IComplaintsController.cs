using ServerAppNetworkForPhotographers.Models.Dtos.Complaints;
using ServerAppNetworkForPhotographers.Models;
using Microsoft.AspNetCore.Mvc;

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
