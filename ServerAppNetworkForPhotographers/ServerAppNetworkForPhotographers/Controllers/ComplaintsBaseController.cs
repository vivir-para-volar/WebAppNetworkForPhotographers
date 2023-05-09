using Microsoft.AspNetCore.Mvc;
using ServerAppNetworkForPhotographers.Exceptions;
using ServerAppNetworkForPhotographers.Exceptions.NotFoundExceptions;
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
            return Ok(await _complaintsService.GetAllComplaintsBase());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComplaintBase>> GetComplaintBaseById(int id)
        {
            return Ok(await _complaintsService.GetComplaintBaseById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ComplaintBase>> CreateComplaintBase(CreateComplaintBaseDto complaintBaseDto)
        {
            ComplaintBase complaintBase;

            try
            {
                complaintBase = await _complaintsService.CreateComplaintBase(complaintBaseDto);
            }
            catch (UniqueFieldException ex)
            {
                return Conflict(ex.Message);
            }

            return CreatedAtAction(nameof(GetComplaintBaseById), new { id = complaintBase.Id }, complaintBase);
        }

        [HttpPut]
        public async Task<ActionResult<ComplaintBase>> UpdateComplaintBase(UpdateComplaintBaseDto complaintBaseDto)
        {
            try
            {
                return await _complaintsService.UpdateComplaintBase(complaintBaseDto);
            }
            catch (ComplaintsBaseNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UniqueFieldException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComplaintBase(int id)
        {
            try
            {
                await _complaintsService.DeleteComplaintBase(id);
            }
            catch (ComplaintsBaseNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }
    }
}
