using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Data;
using ServerAppNetworkForPhotographers.Dtos.Complaints;
using ServerAppNetworkForPhotographers.Interfaces.Controllers;
using ServerAppNetworkForPhotographers.Models;
using ServerAppNetworkForPhotographers.Services;

namespace ServerAppNetworkForPhotographers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintsController : ControllerBase, IComplaintsController
    {
        private readonly ComplaintsService _complaintsService;

        public ComplaintsController(DataContext dataContext)
        {
            _complaintsService = new ComplaintsService(dataContext);
        }

        [HttpGet]
        public async Task<ActionResult<List<Complaint>>> GetAllComplaints()
        {
            return await _complaintsService.GetAllComplaints();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Complaint>> GetComplaintById(int id)
        {
            var complaint = await _complaintsService.GetComplaintById(id);

            return complaint == null ? NotFound() : Ok(complaint);
        }

        [HttpPost]
        public async Task<ActionResult<Complaint>> CreateComplaint(CreateComplaintDto newComplaint)
        {
            Complaint complaint;

            try
            {
                complaint = await _complaintsService.CreateComplaint(newComplaint);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(GetComplaintById), new { id = complaint.Id }, complaint);
        }

        [HttpPut]
        public async Task<ActionResult<Complaint>> UpdateComplaint(UpdateComplaintDto updatedComplaint)
        {
            try
            {
                return await _complaintsService.UpdateComplaint(updatedComplaint);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComplaint(int id)
        {
            try
            {
                await _complaintsService.DeleteComplaint(id);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }
    }
}
