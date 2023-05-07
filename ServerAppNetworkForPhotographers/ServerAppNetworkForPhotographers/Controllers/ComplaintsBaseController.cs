using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Interfaces.Controllers;
using ServerAppNetworkForPhotographers.Models;
using ServerAppNetworkForPhotographers.Models.Contexts;
using ServerAppNetworkForPhotographers.Models.Dtos.ComplaintsBase;
using ServerAppNetworkForPhotographers.Services;

namespace ServerAppNetworkForPhotographers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintsBaseController : ControllerBase, IComplaintsBaseController
    {
        private readonly ComplaintsBaseService _complaintsService;

        public ComplaintsBaseController(DataContext dataContext)
        {
            _complaintsService = new ComplaintsBaseService(dataContext);
        }

        [HttpGet]
        public async Task<ActionResult<List<ComplaintBase>>> GetAllComplaintsBase()
        {
            return await _complaintsService.GetAllComplaintsBase();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComplaintBase>> GetComplaintBaseById(int id)
        {
            var complaintBase = await _complaintsService.GetComplaintBaseById(id);

            return complaintBase == null ? NotFound() : Ok(complaintBase);
        }

        [HttpPost]
        public async Task<ActionResult<ComplaintBase>> CreateComplaintBase(CreateComplaintBaseDto newComplaintBase)
        {
            ComplaintBase complaintBase;

            try
            {
                complaintBase = await _complaintsService.CreateComplaintBase(newComplaintBase);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(GetComplaintBaseById), new { id = complaintBase.Id }, complaintBase);
        }

        [HttpPut]
        public async Task<ActionResult<ComplaintBase>> UpdateComplaintBase(UpdateComplaintBaseDto updatedComplaintBase)
        {
            try
            {
                return await _complaintsService.UpdateComplaintBase(updatedComplaintBase);
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
        public async Task<ActionResult> DeleteComplaintBase(int id)
        {
            try
            {
                await _complaintsService.DeleteComplaintBase(id);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }
    }
}
