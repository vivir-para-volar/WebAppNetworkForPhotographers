using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Dtos.Complaints;
using ServerAppNetworkForPhotographers.Models;

namespace ServerAppNetworkForPhotographers.Interfaces.Controllers
{
    public interface IComplaintsController
    {
        Task<ActionResult<List<Complaint>>> GetAllComplaints();
        Task<ActionResult<Complaint>> GetComplaintById(int id);
        Task<ActionResult<Complaint>> CreateComplaint(CreateComplaintDto newComplaint);
        Task<ActionResult<Complaint>> UpdateComplaint(UpdateComplaintDto updatedComplaint);
        Task<ActionResult> DeleteComplaint(int id);
    }
}
