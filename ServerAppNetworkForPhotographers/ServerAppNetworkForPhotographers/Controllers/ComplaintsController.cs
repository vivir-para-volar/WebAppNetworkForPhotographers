using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Data;
using ServerAppNetworkForPhotographers.Models.Data.Dtos.Complaints;
using ServerAppNetworkForPhotographers.Models.ExceptionsResponses;
using ServerAppNetworkForPhotographers.Models.Lists;
using ServerAppNetworkForPhotographers.Services;

namespace ServerAppNetworkForPhotographers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ComplaintsController : ControllerBase
    {
        private readonly ComplaintsService _complaintsService;

        public ComplaintsController(DataContext dataContext)
        {
            _complaintsService = new ComplaintsService(dataContext);
        }

        [HttpGet("Photographers")]
        [Authorize(Roles = UserRoles.AdminEmployee)]
        public async Task<ActionResult<List<GetPhotographerCountComplaints>>> GetCountPhotographersComplaintsOpen()
        {
            return Ok(await _complaintsService.GetCountPhotographersComplaintsOpen());
        }

        [HttpGet("Photographers/{photographerId}")]
        [Authorize(Roles = UserRoles.AdminEmployee)]
        public async Task<ActionResult<List<GetContentWithCountComplaints>>> GetPhotographerContentsWithCountComplaints(int photographerId)
        {
            return Ok(await _complaintsService.GetPhotographerContentsWithCountComplaints(photographerId));
        }

        [HttpGet("Contents/{contentId}")]
        [Authorize(Roles = UserRoles.AdminEmployee)]
        public async Task<ActionResult<List<Complaint>>> GetComplaintsOpenForContent(int contentId)
        {
            return Ok(await _complaintsService.GetComplaintsOpenForContent(contentId));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = UserRoles.AdminEmployee)]
        public async Task<ActionResult<Complaint?>> GetComplaintById(int id)
        {
            return Ok(await _complaintsService.GetComplaintById(id));
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.User)]
        public async Task<ActionResult<Complaint>> CreateComplaint(CreateComplaintDto complaintDto)
        {
            Complaint complaint;

            try
            {
                complaint = await _complaintsService.CreateComplaint(complaintDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }

            return CreatedAtAction(nameof(GetComplaintById), new { id = complaint.Id }, complaint);
        }

        [HttpPut("Status/{id}")]
        [Authorize(Roles = UserRoles.AdminEmployee)]
        public async Task<ActionResult> UpdateComplaintStatus(int id)
        {
            try
            {
                await _complaintsService.UpdateComplaintStatus(id);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }

            return NoContent();
        }

        [HttpPut("Status/Content/{contentId}")]
        [Authorize(Roles = UserRoles.AdminEmployee)]
        public async Task<ActionResult> UpdateAllComplaintsStatusForContent(int contentId)
        {
            await _complaintsService.UpdateAllComplaintsStatusForContent(contentId);
            return NoContent();
        }
    }
}
