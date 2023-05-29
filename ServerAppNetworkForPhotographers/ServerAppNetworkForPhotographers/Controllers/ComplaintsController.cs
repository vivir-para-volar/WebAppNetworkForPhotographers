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

        [HttpGet]
        [Authorize(Roles = UserRoles.AdminEmployee)]
        public async Task<ActionResult<List<Complaint>>> GetAllComplaintsOpen()
        {
            return Ok(await _complaintsService.GetAllComplaintsOpen());
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
        public async Task<ActionResult<Complaint>> UpdateComplaintStatus(int id)
        {
            try
            {
                return Ok(await _complaintsService.UpdateComplaintStatus(id));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new NotFoundResponse(ex.Message));
            }
        }
    }
}
